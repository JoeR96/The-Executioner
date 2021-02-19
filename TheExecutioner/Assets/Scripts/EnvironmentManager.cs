using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[HideInInspector]
public class EnvironmentManager : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    public Transform NavMeshObject;
    public Transform CubeParent;
    
    public GameObject floorContainer;
    private GameObject[,] _tileArray;

    private EdgeSpawnPlatform edgeSpawnPlatform;
    private EnvironmentSpawner environmentSpawner;
    private WallManager wallManager;
    private RoomManager roomManager;
    
    //coordinates for grid spawner
    public int gridX;
    public int gridZ;
    public int y;
    public int gridSpaceOffset;

    private NavMeshSurface navmeshSurface;
    private List<GameObject[,]> EntranceSpawnTile = new List<GameObject[,]>();
    private List<GameObject[,]> LevelWalls = new List<GameObject[,]>();
    private List<GameObject[,]> LevelRooms = new List<GameObject[,]>();
    private List<GameObject[,]> RandomPlatforms = new List<GameObject[,]>();
    private List<GameObject> Stairs = new List<GameObject>();
    private List<List<GameObject[,]>> LevelPlatforms = new List<List<GameObject[,]>>();
    
    private void Awake()
    {
        navmeshSurface = NavMeshObject.GetComponent<NavMeshSurface>();
        wallManager = GetComponent<WallManager>();
        environmentSpawner = GetComponent<EnvironmentSpawner>();
        roomManager = GetComponent<RoomManager>();
        edgeSpawnPlatform = GetComponent<EdgeSpawnPlatform>();
        
        LevelPlatforms.Add(LevelRooms);
        LevelPlatforms.Add(LevelWalls);
    }

    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
    }
    private void Start()
    {
        _tileArray = environmentSpawner.SpawnGrid(floorContainer, gridX, gridZ, 0, gridSpaceOffset,CubeParent);
        navmeshSurface.BuildNavMesh();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            LowerGroupOfPlatform();
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            navmeshSurface.BuildNavMesh();
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(RaisePlatforms());

        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            var t = roomManager.SpawnRandomRoom(gridX,gridZ);
            LevelRooms.Add(t);
            RaiseAllPlatforms(t);
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
            SpawnStairs();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            StartCoroutine(LowerAllPlatforms());
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            StartCoroutine(SpawnArenaEvent());
        }
    }

    private IEnumerator SpawnArenaEvent()
    {
        var arena = roomManager.SpawnArena(_tileArray);
        LevelRooms.Add(arena);
        foreach (var VARIABLE in arena)
        {
            StartCoroutine(LerpTransformPosition(VARIABLE.transform,
                    new Vector3(VARIABLE.transform.position.x, 4f, VARIABLE.transform.position.z), 0.66f));
        }
        yield return new WaitForSeconds(0.75f);
        navmeshSurface.BuildNavMesh();

            for (int x = 0; x < Random.Range(2,3); x++)
            {
                for (int z = 0; z < Random.Range(2,3); z++)
                { 
                    var spawn = arena[x, z].GetComponent<PlatformState>().spawnPoint;
                    
                    var t = GameManager.instance.ZombieSpawner.SpawnZombie(spawn.transform);
                    NavMeshObject.transform.position  = arena[x, z].gameObject.transform.position;
                    t.transform.position = NavMeshObject.transform.position;
                }
            }

    }

    public GameObject[,] ReturnMap()
    {
        return _tileArray;
    }
    private IEnumerator RaisePlatforms()
    {
        for (int i = 0; i < Random.Range(6,12); i++)
        {
            var t = wallManager.SpawnRandomWall(_tileArray);
            LevelWalls.Add(t);
            RaiseAllPlatforms(t);
        }
        yield return new WaitForSeconds(0.4f);
        navmeshSurface.BuildNavMesh();  
    }

    private void ResetPlatforms()
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

    public void RaisePlatform(GameObject wall)
    {
        StartCoroutine(LerpTransformPosition(wall.transform, new Vector3(wall.transform.position.x,
            wall.transform.position.y + 5f, wall.transform.position.z), 1f));
            
}
    private void RaiseAllPlatforms(GameObject[,] wall)
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
    
    private IEnumerator LowerAllPlatforms()
    {
        foreach (var raisedPlatformGroup in LevelPlatforms)
        {
            foreach (var go in raisedPlatformGroup)
            {
                LowerPlatformSection(go); ;
            }
        }
        
        foreach (var raisedPlatformGroup in LevelPlatforms)
        {
            foreach (var go in raisedPlatformGroup)
            {
                BalanceSection(go);
            }
        }


        foreach (var go in Stairs)
        {
            var targetVector = go.transform.position;
            targetVector.y = -5f;
            StartCoroutine(LerpTransformPosition(go.transform, targetVector, 0.125f));
        }
        yield return new WaitForSeconds(0.25f);
        navmeshSurface.BuildNavMesh();
    }
    private void LowerPlatformSection(GameObject[,] platforms)
    {
        foreach (var platform in platforms)
        {
            StartCoroutine(LerpTransformPosition(platform.transform, new Vector3(platform.transform.position.x,
                 0f, platform.transform.position.z), 1f));
        }
    }
    private void BalanceSection(GameObject[,] platforms)
    {
        float v;
        foreach (var platform in platforms)
        {
            var transformPosition = platform.transform.position;
            v = transformPosition.y;
            float x =Mathf.Round(v);
            transformPosition.y = x;
            platform.transform.position = transformPosition;
        }
    }

    private void LowerGroupOfPlatform()
    {
        var length = LevelWalls.Count;
        var toRemove = length / 3;

        for (int i = 0; i < toRemove; i++)
        {
            var number = Random.Range(0, LevelWalls.Count);
            LowerPlatformSection(LevelWalls[i]);
            LevelWalls.RemoveAt(i);
        }
    }
    public void AddStairToList(GameObject stair)
    {
        Stairs.Add(stair);
    }
    private void SpawnStairs()
    {
        foreach (var go in LevelPlatforms)
        {
            for (int i = 0; i < Random.Range(3,5); i++)
            {
                var random = Random.Range(0, LevelWalls.Count );
                var randomX = Random.Range(0, LevelWalls[random].GetLength(0));
                var randomZ = Random.Range(0, LevelWalls[random].GetLength(1));
                LevelWalls[random][randomX,randomZ].GetComponent<PlatformState>().SpawnStairs();
            }
        }

    }
    
    
    
    protected IEnumerator LerpTransformPosition(Transform transform, Vector3 target, float duration)
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

        transform.position = target;
    }

    
    private Tuple<int,int> ReturnPosition()
    {
        int x  = Random.Range(0,15);
        int z = Random.Range(0,15); ;
        return new Tuple<int, int>(x, z);
    }
}


