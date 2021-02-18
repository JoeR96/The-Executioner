using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnvironmentManager : MonoBehaviour
{
    public Transform SpawnPoint;
    public int gridX;
    public int gridZ;
    public int y;
    public float gridSpaceOffset;
    public GameObject floorContainer;
    public GameObject[,] tileArray;

    private List<GameObject[,]> EntranceSpawnTile = new List<GameObject[,]>();
    private List<GameObject[,]> LevelWalls = new List<GameObject[,]>();
    private List<GameObject[,]> LevelRooms = new List<GameObject[,]>();
    private List<GameObject[,]> RandomPlatforms = new List<GameObject[,]>();
    private List<List<GameObject[,]>> LevelPlatforms = new List<List<GameObject[,]>>();

    protected void Awake()
    {

        tileArray = new GameObject[gridX,gridZ];
        SpawnGrid();
    }

    private void Start()
    {
        PopulateEntranceSpawners();
        var t = Random.Range(0, EntranceSpawnTile.Count);
        RaisePlatformSection(EntranceSpawnTile[t]);
        SpawnWall();
        var x = Random.Range(8, 15);
        for (int i = 0; i < t; i++)
        {
            SpawnWall();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            var x = Random.Range(8, 15);
            for (int i = 0; i < x; i++)
            {
                SpawnWall();
            }
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < Random.Range(0,4); i++)
            {
                SpawnRandomRoom();
            }
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            foreach (var go in LevelRooms)
            {
                LowerPlatformSection(go);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            foreach (var go in LevelWalls)
            {
                LowerPlatformSection(go);
            }
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            StartCoroutine(SpawnArena());
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            LowerAllPlatform();
        }
    }

    private void LowerAllPlatform()
    {
        foreach (var go in LevelWalls)
        {
            LowerPlatformSection(go);
        }

        foreach (var go in LevelRooms)
        {
            LowerPlatformSection(go);
        }
    }

    protected void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpaceOffset,y, z * gridSpaceOffset );
                tileArray[x,z] = SpawnChunk(spawnPosition,quaternion.identity);
                tileArray[x,z].GetComponent<PlatformState>().Setint(x,z);
            }
        }
    }

    private void PopulateEntranceSpawners()
    {
        var a = FillRoomArray(0, 0,2,2);
        var b = FillRoomArray(5, 0,2,2);
        var c = FillRoomArray(0, 5,2,2);
        var d = FillRoomArray(5, 5,2,2);
        
        EntranceSpawnTile.Add(a);
        EntranceSpawnTile.Add(b);
        EntranceSpawnTile.Add(c);
        EntranceSpawnTile.Add(d);
    }

    private void SpawnRandomRoom()
    {
        var RoomSize = GetRandomRoomSize();
        var RoomPositions = GetRoomStartPosition();
        
        int xSpawn = RoomPositions.Item1;
        int zSpawn = RoomPositions.Item2;

        int xSize = RoomSize.Item1;
        int zSize = RoomSize.Item2;

        var room = FillRoomArray(xSpawn, zSpawn, xSize, zSize);
        LevelRooms.Add(room);

        RaisePlatformArray(room);

    }
    private GameObject[,] FillRoomArray(int targetX, int targetZ,int roomXsize, int roomZsize)
    {
        GameObject[,] room = new GameObject[roomXsize,roomZsize];
        
        for (int x = 0; x < roomXsize; x++)
        {
            for (int z = 0; z < roomZsize; z++)
            {
                room[x, z] = tileArray[targetX + x, targetZ + z];
            }
        }
        return room;
    }
    
    private void FillWallArray(int xWallLength, int zWallLength,int xStart, int zStart)
    {
        GameObject[,] wall = new GameObject[xWallLength,zWallLength];
        for (int x = 0; x < xWallLength ; x++)
        {
            for (int z = 0; z < zWallLength ; z++)
            {
               
                if (Random.value > 0.5f)
                {
                    wall[x, z] = tileArray[xStart , zStart + z];
                }
                else
                {
                    wall[x, z] = tileArray[xStart + x, zStart];    
                }

            }
        }
        LevelWalls.Add(wall);

        RaisePlatformArray(wall);
 
    }

    private void RaisePlatformArray(GameObject[,] wall)
    {
        foreach (var go in wall)
        {
            if (go)
            {
                StartCoroutine(LerpTransformPosition(go.transform, new Vector3(go.transform.position.x,
                    go.transform.position.y + 5f
                    , go.transform.position.z), 1f));
            }
        }
    }

    private void SpawnWall( )
    {
        int startX; 
        startX = Random.Range(2, 14);
        int startZ; 
        startZ = Random.Range(2, 14);
        //get length of wall
        int wallOneLength = Random.Range(1, 5);
        int wallTwoLength = Random.Range(1, 5);
        FillWallArray(wallOneLength, wallTwoLength, startX, startZ);

    }
    private void RaisePlatformSection(GameObject[,] platforms)
    {
        foreach (var platform in platforms)
        {
            StartCoroutine(LerpTransformPosition(platform.transform, new Vector3(platform.transform.position.x,
                platform.transform.position.y + 5f
                , platform.transform.position.z), 1f));
        }
    }
    private void RaisePlatforms(Transform currentPosition)
    {
        Vector3 targetPosition = currentPosition.position;
        targetPosition.y = targetPosition.y += 5f;
        StartCoroutine(LerpTransformPosition(currentPosition,targetPosition, 0.5f));
    }
    [SerializeField] private GameObject _raycastHolder;
    private GameObject FireRay()
    {
        RaycastHit hit;
        float thickness = 1f; //<-- Desired thickness here.
        Vector3 origin = _raycastHolder.transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        
        if (Physics.SphereCast(origin, thickness, direction, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
    private IEnumerator RaisePlatform(int pathLength)
    {
        for (int i = 0; i < pathLength; i++)
        {
        
            var platform = FireRay();
            var platformState = platform.GetComponent<PlatformState>();

            if (!platformState) 
            {
                yield break;
            }
            
            if (platformState.ReturnState())
            {
                yield return null;
            }

            var coords = ReturnPosition();
            var x = coords.Item1;
            var z = coords.Item2;
            
            SetPosition(x,z);
            RaisePlatforms(platform.transform);
            platformState.SetState();
            yield return new WaitForSeconds(0.125f);
        
        }
    }
    private void SetPosition(int x, int z)
    {
        var tiles = tileArray;
        var position = tiles[x,z].transform.position;
        var offsetPosition =  new Vector3(position.x, 45f, position.z);
        _raycastHolder.transform.position = offsetPosition;
    }
    private void LowerPlatformSection(GameObject[,] platforms)
    {
        foreach (var platform in platforms)
        {
            StartCoroutine(LerpTransformPosition(platform.transform, new Vector3(platform.transform.position.x,
                 0f, platform.transform.position.z), 1f));
        }
    }

    private void LowerAllPlatforms()
    {
        foreach (var t in LevelPlatforms)
        {
            for (int i = 0; i < t.Count; i++)
            {
                LowerPlatformSection(t[i]);
            }
        }
    }

    private IEnumerator SpawnArena()
    {

        int xSpawn = Random.Range(3, 7);
        int zSpawn = Random.Range(3, 7);

        int xSize = Random.Range(7, 10);
        int zSize = Random.Range(8, 14);

        var room = FillRoomArray(xSpawn, zSpawn, xSize, zSize);
        LevelRooms.Add(room);
        LowerAllPlatform();
        yield return new WaitForSeconds(1f);
        RaisePlatformArray(room);

    }
    
    protected GameObject SpawnChunk(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        var chunk = Instantiate(floorContainer, positionToSpawn, rotationToSpawn);
        return chunk;
    }
    
    public IEnumerator LerpTransformPosition(Transform transform, Vector3 target, float duration)
    {
        Transform startPosition = transform;
        float timer = 0f;
        float _duration = duration;
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            transform.position = Vector3.Lerp(startPosition.position, target, percentage);
            yield return null;
        }
    }
    private Tuple<int,int> GetRoomStartPosition()
    {
        int roomX = Random.Range(1, gridX - 1);
        int roomZ = Random.Range(1, gridZ - 1);
        return new Tuple<int, int>(roomX, roomZ);
    }
    
    private Tuple<int,int> GetRandomRoomSize()
    {
        int roomSizeX = Random.Range(2, 5);
        int roomSizeZ = Random.Range(2, 7);
        return new Tuple<int, int>(roomSizeX, roomSizeZ);
    }
    
    private Tuple<int,int> ReturnPosition()
    {
        int x  = Random.Range(0,15);
        int z = Random.Range(0,15); ;
        return new Tuple<int, int>(x, z);
    }
}


