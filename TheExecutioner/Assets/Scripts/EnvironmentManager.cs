using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private Pathfinding pathFinding;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    private NavMeshSurface navmeshSurface;
    public PlatformManager platformManager;
    private EnvironmentSpawner environmentSpawner;
    private Grid grid;
=======
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    //coordinates for grid spawner
    public int gridX;
    public int gridZ;
    public int y;
    public int gridSpaceOffset;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)

    private NavMeshSurface navmeshSurface;
    private List<GameObject[,]> EntranceSpawnTile = new List<GameObject[,]>();
    private List<GameObject[,]> LevelWalls = new List<GameObject[,]>();
    private List<GameObject[,]> LevelRooms = new List<GameObject[,]>();
    private List<GameObject[,]> RandomPlatforms = new List<GameObject[,]>();
    private List<GameObject> Stairs = new List<GameObject>();
    private List<List<GameObject[,]>> LevelPlatforms = new List<List<GameObject[,]>>();
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    public List<List<Node>> LevelPaths = new List<List<Node>>();

    public void ClearPaths()
    {
        LowerAllPathPlatforms();
        LevelPaths.Clear();
    }

=======
    private List<List<Node>> LevelPaths = new List<List<Node>>();
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
    private List<List<Node>> LevelPaths = new List<List<Node>>();
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
    private List<List<Node>> LevelPaths = new List<List<Node>>();
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
    private List<List<Node>> LevelPaths = new List<List<Node>>();
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    //public GameObject floorContainer;
    //Instantiate(floorContainer, n.worldPosition, quaternion.identity);
    private void Awake()
    {
        pathFinding = GetComponent<Pathfinding>();
        navmeshSurface = NavMeshObject.GetComponent<NavMeshSurface>();
        wallManager = GetComponent<WallManager>();
        environmentSpawner = GetComponent<EnvironmentSpawner>();
        roomManager = GetComponent<RoomManager>();
        edgeSpawnPlatform = GetComponent<EdgeSpawnPlatform>();
        
        LevelPlatforms.Add(LevelRooms);
        LevelPlatforms.Add(LevelWalls);
<<<<<<< HEAD
    }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    
    private void ChangePathColor(Material material,List<Node> list)
    {
        foreach (var g in list)
            {
                g.platform.GetComponent<MeshRenderer>().material.color = material.color;
            }
        
    }

    [SerializeField] private Material[] Colours;
    public void SpawnPaths()
    {
        var random = Random.Range(0, Colours.Length);
        var path = GetPath(LevelPaths);
        foreach (var t in path)
        {
            if (t.InUse)
            {
                path.
            }
            t.InUse = true;
        }
        pathFinding.InitializePath();
        ChangePathColor(Colours[random], path);
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.F12))
=======

    public void BuildNavMesh()
    {
=======

    public void BuildNavMesh()
    {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======

    public void BuildNavMesh()
    {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
        navmeshSurface.BuildNavMesh();
    }
    private void Start()
    {
        //_tileArray = environmentSpawner.SpawnGrid(floorContainer, gridX, gridZ, 0, gridSpaceOffset,CubeParent);
        //navmeshSurface.BuildNavMesh();
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
            StartCoroutine((SpawnWallPlatforms()));
        }
        if (Input.GetKeyDown(KeyCode.F3))
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
        {
            foreach (var go in LevelPaths)
            {
                foreach (var VARIABLE in go)
                {
                    GameManager.instance.EnvironmentManager.LowerPlatform(VARIABLE.platform);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetPath();
            pathFinding.InitializePath();
        }
        if (Input.GetKey(KeyCode.V))
        {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            RaiseAllPathPlatforms();
=======
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
            foreach (var go in LevelPaths)
            {
                foreach (var VARIABLE in go)
                {
                    GameManager.instance.EnvironmentManager.RaisePlatform(VARIABLE.platform);
                }
            }
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
        }
<<<<<<< HEAD
<<<<<<< HEAD
    }
<<<<<<< HEAD
    public void RaiseAllPathPlatforms()
    {
        foreach (var go in LevelPaths)
        {
            foreach (var VARIABLE in go)
            {
                platformManager.RaisePlatform(VARIABLE.platform);
                var nodeInUse = VARIABLE.platform.GetComponent<PlatformState>();
                nodeInUse.PlatformIsActive = true;
            }
        }
    }
    public void LowerAllPathPlatforms()
    {
        foreach (var go in LevelPaths)
        {
            foreach (var VARIABLE in go)
            {
                platformManager.LowerPlatform(VARIABLE.platform);
                var nodeInUse = VARIABLE.platform.GetComponent<PlatformState>();
                nodeInUse.PlatformIsActive = true;
            }
        }
    }
    void SmoothMap()
=======

    private IEnumerator SpawnWallPlatforms()
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    {
        yield return null;
    }
    private IEnumerator SpawnArenaEvent()
    {
        var arena = roomManager.SpawnArena(_tileArray);
        LevelRooms.Add(arena);
        foreach (var VARIABLE in arena)
        {
=======
    }

    private IEnumerator SpawnWallPlatforms()
    {
        yield return null;
    }
    private IEnumerator SpawnArenaEvent()
    {
        var arena = roomManager.SpawnArena(_tileArray);
        LevelRooms.Add(arena);
        foreach (var VARIABLE in arena)
        {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
    }

    private IEnumerator SpawnWallPlatforms()
    {
        yield return null;
    }
    private IEnumerator SpawnArenaEvent()
    {
        var arena = roomManager.SpawnArena(_tileArray);
        LevelRooms.Add(arena);
        foreach (var VARIABLE in arena)
        {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
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

=======
    }

    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
    }
    private void Start()
    {
        //_tileArray = environmentSpawner.SpawnGrid(floorContainer, gridX, gridZ, 0, gridSpaceOffset,CubeParent);
        //navmeshSurface.BuildNavMesh();
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
            StartCoroutine((SpawnWallPlatforms()));
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            foreach (var go in LevelPaths)
            {
                foreach (var VARIABLE in go)
                {
                    GameManager.instance.EnvironmentManager.LowerPlatform(VARIABLE.platform);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetPath();
            pathFinding.InitializePath();
        }
        if (Input.GetKey(KeyCode.V))
        {
            foreach (var go in LevelPaths)
            {
                foreach (var VARIABLE in go)
                {
                    GameManager.instance.EnvironmentManager.RaisePlatform(VARIABLE.platform);
                }
            }
        }
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    }
<<<<<<< HEAD
    public void BuildNavMesh()
=======

<<<<<<< HEAD
    public GameObject[,] ReturnMap()
    {
        return _tileArray;
    }
    private IEnumerator RaisePlatforms()
    {
=======
    private IEnumerator SpawnWallPlatforms()
    {
        yield return null;
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
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
        for (int i = 0; i < Random.Range(6,12); i++)
        {
            var t = wallManager.SpawnRandomWall(_tileArray);
            LevelWalls.Add(t);
            RaiseAllPlatforms(t);
        }
        yield return new WaitForSeconds(0.4f);
        navmeshSurface.BuildNavMesh();  
    }

    private IEnumerator LowerPlatforms()
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    {
        foreach (var go in LevelPaths)
        {
            foreach (var node in go)
            {
                LowerPlatform(node.platform);
            }
            
        }
        yield return new WaitForSeconds(0.4f);
        navmeshSurface.BuildNavMesh(); 
    }
<<<<<<< HEAD
<<<<<<< HEAD
    public void SpawnStairs()
=======

    private void ResetPlatforms()
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    {
        foreach (var go in LevelWalls)
        {
<<<<<<< HEAD
            var path = ReturnRandomPath();
                var node = GetRandomNode(path);
                var adjacent = CheckAdjacentPositions(node);

                    if (!ReturnStairSpawnStatus(2, 1, adjacent))
                    {
                        if(!ReturnStairSpawnStatus(2,0,adjacent))
                        {
                            var t = Instantiate(stairs, adjacent[2,1].platform.GetComponent<PlatformState>().spawnPoint.transform.position, 
                                quaternion.identity);
                            t.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y , transform.localRotation.eulerAngles.z);
                        }
                    }
                    if (!ReturnStairSpawnStatus(1, 2, adjacent))
                    {
                        if(!ReturnStairSpawnStatus(0,2,adjacent))
                        {
                            var t = Instantiate(stairs, adjacent[1,2].platform.GetComponent<PlatformState>().spawnPoint.transform.position, 
                                quaternion.identity);
                            t.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + 90, transform.localRotation.eulerAngles.z);
                        }
                    }
                    if (!ReturnStairSpawnStatus(3, 2, adjacent))
                    {
                        if(!ReturnStairSpawnStatus(4,2,adjacent))
                        {
                           var t = Instantiate(stairs, adjacent[3,2].platform.GetComponent<PlatformState>().spawnPoint.transform.position, 
                                quaternion.identity);
                           t.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + 180, transform.localRotation.eulerAngles.z);
                        }
                    }
                    if (!ReturnStairSpawnStatus(2, 3, adjacent))
                    {
                        if(!ReturnStairSpawnStatus(2,4,adjacent))
                        {
                           var t = Instantiate(stairs, adjacent[2,3].platform.GetComponent<PlatformState>().spawnPoint.transform.position, 
                                quaternion.identity);
                           t.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + 270, transform.localRotation.eulerAngles.z);
                        }
                    }

        }
    }
    private bool ReturnStairSpawnStatus(int x,int y,Node[,] platformGroup)
    {
        return platformGroup[ x,  y].InUse;
    }
    private Node[,] CheckAdjacentPositions(Node node)
=======
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
    public void LowerPlatform(GameObject wall)
    {
        StartCoroutine(LerpTransformPosition(wall.transform, new Vector3(wall.transform.position.x,0f, wall.transform.position.z), 1f));
            
    }
    private void RaiseAllPlatforms(GameObject[,] wall)
    {
=======
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
    public void LowerPlatform(GameObject wall)
    {
        StartCoroutine(LerpTransformPosition(wall.transform, new Vector3(wall.transform.position.x,0f, wall.transform.position.z), 1f));
            
    }
    private void RaiseAllPlatforms(GameObject[,] wall)
    {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
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
    public void LowerPlatform(GameObject wall)
    {
        StartCoroutine(LerpTransformPosition(wall.transform, new Vector3(wall.transform.position.x,0f, wall.transform.position.z), 1f));
            
    }
    private void RaiseAllPlatforms(GameObject[,] wall)
    {
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
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
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
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

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    private Node GetRandomNode(List<Node> nodes)
=======
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======

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
    public void LowerPlatform(GameObject wall)
    {
        StartCoroutine(LerpTransformPosition(wall.transform, new Vector3(wall.transform.position.x,0f, wall.transform.position.z), 1f));
            
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

>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)

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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    {
        foreach (var platform in platforms)
        {
            StartCoroutine(LerpTransformPosition(platform.transform, new Vector3(platform.transform.position.x,
                 0f, platform.transform.position.z), 1f));
        }
    }
    private void BalanceSection(GameObject[,] platforms)
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        var random = Random.Range(0, LevelPaths.Count);
        var path = LevelPaths[random];
        foreach (var go in path)
        {
            go.InUse = true;
        }
        return path;
=======
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
        float v;
        foreach (var platform in platforms)
        {
            var transformPosition = platform.transform.position;
            v = transformPosition.y;
            float x =Mathf.Round(v);
            transformPosition.y = x;
            platform.transform.position = transformPosition;
        }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
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

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    private List<Node> GetPath(List<List<Node>> nodes)
    {
        var t = pathFinding.ReturnPath();
        
        foreach (var go in nodes)
        {
            foreach (var node in go)
            {
                node.InUse = true;
            }
        }
        
        return t;
    }

    public void RaiseAllPaths()
    {
        
=======
    private void GetPath()
    {
        LevelPaths.Add(pathFinding.ReturnPath());
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
>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    }

=======
    private void GetPath()
    {
        LevelPaths.Add(pathFinding.ReturnPath());
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

>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
    private void GetPath()
    {
        LevelPaths.Add(pathFinding.ReturnPath());
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

>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
=======
    private void GetPath()
    {
        LevelPaths.Add(pathFinding.ReturnPath());
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

>>>>>>> parent of 0475716 (Stairs spawn in proper position + added 2 deep check)
    
  
}


