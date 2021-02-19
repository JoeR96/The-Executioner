using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
            
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            StartCoroutine(LowerAllPlatforms());
        }
    }

    public GameObject[,] ReturnMap()
    {
        return _tileArray;
    }
    private IEnumerator RaisePlatforms()
    {
        for (int i = 0; i < Random.Range(9,15); i++)
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
                go.GetComponent<PlatformState>().SetState(true);
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
            Debug.Log("1");
            foreach (var go in raisedPlatformGroup)
            {
                Debug.Log("2");
                LowerPlatformSection(go);
            }
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
    }

    
    private Tuple<int,int> ReturnPosition()
    {
        int x  = Random.Range(0,15);
        int z = Random.Range(0,15); ;
        return new Tuple<int, int>(x, z);
    }
}


