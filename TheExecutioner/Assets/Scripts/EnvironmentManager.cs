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
using UnityEngine.ProBuilder;
using Random = UnityEngine.Random;



[HideInInspector]
public class EnvironmentManager : MonoBehaviour
{

    public GameObject stairs;
    public Transform NavMeshObject;
    private GameObject[,] _tileArray;
    private WallManager wallManager;
    private RoomManager roomManager;
    private Pathfinding pathFinding;
    private NavMeshSurface navmeshSurface;
    public PlatformManager PlatformManager;
    private EnvironmentSpawner environmentSpawner;
    private Grid grid;

    private List<GameObject[,]> EntranceSpawnTile = new List<GameObject[,]>();
    private List<GameObject[,]> LevelWalls = new List<GameObject[,]>();
    private List<GameObject[,]> LevelRooms = new List<GameObject[,]>();
    private List<GameObject[,]> RandomPlatforms = new List<GameObject[,]>();
    private List<GameObject> Stairs = new List<GameObject>();
    private List<List<GameObject[,]>> LevelPlatforms = new List<List<GameObject[,]>>();
    public List<List<Node>> LevelPaths = new List<List<Node>>();
    public List<List<Node>> LevelBunkers = new List<List<Node>>();

    public Material Blue;
    public Material Yellow;


    public void LowerAll()
    {
        PlatformManager.LowerMultiplePlatformSection(LevelBunkers);
        PlatformManager.LowerMultiplePlatformSection(LevelPaths);
    }

    private void Awake()
    {
        environmentSpawner = GetComponent<EnvironmentSpawner>();
        pathFinding = GetComponent<Pathfinding>();
        navmeshSurface = NavMeshObject.GetComponent<NavMeshSurface>();
        wallManager = GetComponent<WallManager>();
        roomManager = GetComponent<RoomManager>();
        PlatformManager = GetComponent<PlatformManager>();
        LevelPlatforms.Add(LevelRooms);
        LevelPlatforms.Add(LevelWalls);
        grid = GetComponent<Grid>();
    }
    
    private void ChangePathColor(Material material,List<List<Node>> list)
    {
        foreach (var go in list)
        {
            foreach (var g in go)
            {
                g.platform.GetComponent<MeshRenderer>().material = material;
            }
        }
    }

    public Material[] Materials;

    public void StartBunkers()
    {
        SpawnBunkers(LevelBunkers);
    }
    public void SpawnBunkers(List<List<Node>> Levels)
    {
        var path = GetPath();

        Levels.Add(path);
        pathFinding.InitializePath();
        var material = Random.Range(0, Materials.Length);
        //ChangePathColor(Materials[material],LevelBunkers);
        foreach (var go in Levels)
        {    
            foreach (var VARIABLE in go)
            {
                if (!VARIABLE.InUse)
                {
                    VARIABLE.platform.GetComponent<PlatformState>().PlatformIsActive = true;
                    VARIABLE.platform.GetComponent<MeshRenderer>().material = Materials[material]; 
                    PlatformManager.RaisePlatform(VARIABLE.platform);
                }
                    
            }
        }
        SpawnStairs(path);
        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            navmeshSurface.BuildNavMesh();
        }


        if (Input.GetKeyDown(KeyCode.V))
        {

            foreach (var go in LevelPaths)
            {
                foreach (var VARIABLE in go)
                {
                    PlatformManager.RaisePlatform(VARIABLE.platform);
                    var nodeInUse = VARIABLE.platform.GetComponent<PlatformState>();
                    nodeInUse.PlatformIsActive = true;
                }
            }
        }

    }

    public void SmoothMap()
    {

        List<Node> platformsToSmooth = new List<Node>();
        
        for (int x = 0; x < grid.gridSizeX - 1 ; x++)
        {
            for (int y = 0; y < grid.gridSizeY - 1  ; y++)
            {
                var centre = grid.grid[x, y].platform.transform.position.y;
                var t = CheckAdjacentPositions(grid.grid[x, y]);
                var temp = t;

                foreach (var node in temp)
                {
                    //check surrounding nodes are lower than current Y positions
                    var adjacent = node.platform.transform.position.y;
                    var nodeInUse = node.platform.GetComponent<PlatformState>();

                    if (adjacent > centre && !nodeInUse.PlatformIsActive)
                    {
                        platformsToSmooth.Add(node);
                        //n1odeInUse.PlatformIsActive = true;
                    }
                }
                
                if (platformsToSmooth.Count > 4)
                {
                    PlatformManager.RaisePath(platformsToSmooth);
                }

            }
        }
    }

    private bool IsplatformIsBelow()
    {
        {
            return false;
        }
    }

    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
    }

    public void SpawnStairs(List<Node> path)
    {
        List<Tuple<Vector2, float>> spawnPositions = new List<Tuple<Vector2, float>>();

        Tuple<Vector2, float> GetStairSpawn(Vector2 position, float rotation)
        {
            float quaternion = rotation;
            var vector = position;
            return new Tuple<Vector2, float>(vector, quaternion);
        }

        var node = GetNode(path);
        var adjacent = CheckAdjacentPositions(node);

        //The purpose of checking twice here is to ensure there is nothing blocking the platform leading to the stairs
        if (!ReturnStairSpawnStatus(2, 1, adjacent))
        {
            if (!ReturnStairSpawnStatus(2, 0, adjacent))
            {
                spawnPositions.Add(GetStairSpawn(new Vector2(2, 1), 0));
            }
        }
        if (!ReturnStairSpawnStatus(1, 2, adjacent))
        {
            if (!ReturnStairSpawnStatus(0, 2, adjacent))
            {
                spawnPositions.Add(GetStairSpawn(new Vector2(1, 2), 90));
            }
        }
        if (!ReturnStairSpawnStatus(3, 2, adjacent))
        {
            if (!ReturnStairSpawnStatus(4, 2, adjacent))
            {
                spawnPositions.Add(GetStairSpawn(new Vector2(3, 2), 270));
            }
        }
        if (!ReturnStairSpawnStatus(2, 3, adjacent))
        {
            if (!ReturnStairSpawnStatus(2, 4, adjacent))
            {
                spawnPositions.Add(GetStairSpawn(new Vector2(2, 3), 180));
            }
        }


        if (spawnPositions.Count != 0)
        {
            var random = Random.Range(0, spawnPositions.Count);
            var spawnPosition = spawnPositions[random];
            var pos = spawnPosition.Item1;

            var t = Instantiate(stairs,
                adjacent[(int) pos.x, (int) pos.y].worldPosition,
                quaternion.identity);

            var x = t.GetComponent<PlatformState>();
            x.PlatformIsActive = true;
            foreach (var adjacentt in adjacent)
            {
                adjacentt.platform.GetComponent<MeshRenderer>().material = Materials[2];
                if(!adjacentt.platform.GetComponent<PlatformState>().PlatformIsActive)
                {
                    adjacentt.platform.GetComponent<MeshRenderer>().material = Materials[3];
                }
            }
            adjacent[2, 2].platform.GetComponent<MeshRenderer>().material = Materials[2];
            x.Setint(adjacent[0,0].gridX -1 +(int)pos.x,adjacent[0,0].gridY+(int)pos.y -1);
             t.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y + spawnPosition.Item2,
                transform.localRotation.eulerAngles.z);
             var newPos = new Vector3(t.transform.position.x + 2, t.transform.position.y +2.5f, t.transform.position.z - 2);
             t.transform.position = newPos;
        }
    }
    private Node GetNode(List<Node> path)
    {
        return GetRandomNode(path);
    }

    private bool ReturnStairSpawnStatus(int x,int y,Node[,] platformGroup)
    {
        if (platformGroup[x, y] == null)
        {
            foreach(var go in platformGroup)
                Debug.Log(go.GridPosition);
            
            Debug.Log(platformGroup[2,2]);
            Debug.Log(x);
            Debug.Log(y);
        }
        return platformGroup[ x,  y].platform.GetComponent<PlatformState>().PlatformIsActive;
    }
    
    
    private Node[,] CheckAdjacentPositions(Node node)
    {
        
        Node[,] adjacent = new Node[5,5];

        var startX = node.gridX -2;
        var startZ = node.gridY -2;
        
        for (int x = 0; x < adjacent.GetLength(0) ; x++)
        {
            for (int z = 0; z < adjacent.GetLength(1) ; z++)
            {
      
                adjacent[x , z ] = grid.grid[startX +x, startZ + z];
                if(grid.grid[startX + x, startZ + z] == null)
                {
                    Debug.Log(startX);
                    Debug.Log(x);
                    Debug.Log(startZ);
                    Debug.Log(z);
                }
            }
        }
        return adjacent;
    }
    
    private Node GetRandomNode(List<Node> nodes)
    {
        var random = Random.Range(0, nodes.Count);
        var node = nodes[random];
        if (node == null)
        {
            Debug.Log(nodes.Count);
            Debug.Log(random);
        }
        return node;
    }
    private List<Node> ReturnRandomPath()
    {
        var random = Random.Range(0, LevelBunkers.Count);
        var path = LevelBunkers[random];
        return path;
    }

    public GameObject[,] ReturnMap()
    {
        return _tileArray;
    }

    private List<Node> GetPath()
    {
        var path = pathFinding.ReturnPath();
        return path;
    }
    
    public void LowerLastPlatform()
    {
        StartCoroutine(LowerNegativePlatform());
    }
    public IEnumerator LowerNegativePlatform()
    {
        var path = ReturnRandomPath();
        for (int i = path.Count - 1; i >= 0; i--)
        {
            PlatformManager.LowerPlatform(path[i].platform);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
    


