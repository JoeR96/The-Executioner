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

    public GameObject stairs;
    public Transform NavMeshObject;
    private GameObject[,] _tileArray;
    private WallManager wallManager;
    private RoomManager roomManager;
    private Pathfinding pathFinding;
    private NavMeshSurface navmeshSurface;
    private PlatformManager platformManager;
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


    //public GameObject floorContainer;
    //Instantiate(floorContainer, n.worldPosition, quaternion.identity);
    private void Awake()
    {
        environmentSpawner = GetComponent<EnvironmentSpawner>();
        pathFinding = GetComponent<Pathfinding>();
        navmeshSurface = NavMeshObject.GetComponent<NavMeshSurface>();
        wallManager = GetComponent<WallManager>();
        roomManager = GetComponent<RoomManager>();
        platformManager = GetComponent<PlatformManager>();
        LevelPlatforms.Add(LevelRooms);
        LevelPlatforms.Add(LevelWalls);
        grid = GetComponent<Grid>();
    }

 
    private void Start()
    {
        for (int i = 0; i < Random.Range(2,5); i++)
        {
            SpawnBunkers();
        }
         // for (int i = 0; i < Random.Range(2,5); i++)
         // {
         //     SpawnPaths();
         // }
        

        
        

        // _tileArray = environmentSpawner.SpawnGrid(floorContainer, gridX, gridZ, 0, gridSpaceOffset,CubeParent);
        //navmeshSurface.BuildNavMesh();
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
    private void SpawnPaths()
    {
        GetPath(LevelPaths);
        pathFinding.InitializePath();
        SpawnStairs();
        ChangePathColor(Blue, LevelBunkers);
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
    
    private void SpawnBunkers()
    {
        GetPath(LevelBunkers);
        pathFinding.InitializePath();
        SpawnStairs();
        ChangePathColor(Yellow,LevelPaths);
        foreach (var go in LevelBunkers)
        {
            foreach (var VARIABLE in go)
            {
                platformManager.RaisePlatform(VARIABLE.platform);
                var nodeInUse = VARIABLE.platform.GetComponent<PlatformState>();
                nodeInUse.PlatformIsActive = true;
            }
        }
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.F1))
        {
            SpawnPaths();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SpawnBunkers();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            platformManager.LowerMultiplePlatformSection(LevelPaths);
            platformManager.LowerMultiplePlatformSection(LevelBunkers);
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            navmeshSurface.BuildNavMesh();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
    
        }

        if (Input.GetKeyDown(KeyCode.V))
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

        if (Input.GetKeyDown(KeyCode.B))
        {
            SmoothMap();
        }

    }

    void SmoothMap()
    {

        List<Node> platformsToSmooth = new List<Node>();


       
        for (int x = 0; x < grid.gridSizeX - 1; x++)
        {
            for (int y = 0; y < grid.gridSizeY - 1; y++)
            {
                var centre = grid.grid[x, y].platform.transform.position.y;
                var t = grid.GetNeighbours(grid.grid[x, y]);
                var temp = t;

                foreach (var node in temp)
                {
                    //check surrounding nodes are lower than current Y positions
                    var adjacent = node.platform.transform.position.y;
                    var nodeInUse = node.platform.GetComponent<PlatformState>();
                    Debug.Log(nodeInUse.PlatformIsActive);
                    Debug.Log(adjacent < centre && !nodeInUse.PlatformIsActive);
                    if (adjacent > centre && !nodeInUse.PlatformIsActive)
                    {
                        platformsToSmooth.Add(node);
                        nodeInUse.PlatformIsActive = true;
                    }
                }

                //if more than 4 nodes raise the platform to the same level
                //will need to check the height of the node 
                if (platformsToSmooth.Count > 4)
                {

                    platformManager.RaisePath(platformsToSmooth);
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

    private void SpawnStairs()
    {
        List<Node> stairSpawns = new List<Node>();
        for (int i = 0; i < LevelPaths.Count; i++)
        {
            var path = ReturnRandomPath();
                var node = GetRandomNode(path);
                var adjacent = CheckAdjacentPositions(node);
                
                //The purpose of checking twice here is to ensure there is nothing blocking the platform leading to the stairs
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
                
                
             
                    
                    //GameManager.instance.EnvironmentManager.platformManager.RaisePlatform(go.platform);
                
                //Vector3 spawnPosition = new Vector3(spawnPos.GridPosition.x +3f, 20f, spawnPos.GridPosition.y);
                
                //y.transform.rotation *= Quaternion.Euler(0,90,0);
            
        }
    }

    private bool ReturnStairSpawnStatus(int x,int y,Node[,] platformGroup)
    {
        return platformGroup[ x,  y].InUse;
    }
    private static Vector3 SpawnPos(List<Node> stairSpawns, int random)
    {
        var spawnPos = stairSpawns[random].worldPosition;
        return spawnPos;
    }

    private Node[,] CheckAdjacentPosition(Node node)
    {
      
        Node[,] adjacent;
        adjacent = new Node[3, 3];

        int counterr = 0;
        var gridX = node.gridX -1;
        var gridY = node.gridY -1;


        int counter = 0;
        for (int x = 0; x < adjacent.GetLength(0) ; x++)
        {
            for (int z = 0; z < adjacent.GetLength(1); z++)
            {
                Debug.Log(grid.grid.Length);
                var t  = grid.grid[x + gridX, z + gridY];
                adjacent[z, x] = t;
                // Debug.Log(grid.grid[x,z].gridX + " + " + grid.grid[x,z].gridY);
                // Debug.Log(gridX + " " + gridY);
                    counter++;

            }
        }

        return adjacent;
    }

    private Node[,] CheckAdjacentPositions(Node node)
    {
        
        Node[,] adjacent = new Node[5,5];

        var startX = node.platform.GetComponent<PlatformState>().X -1;
        var startZ = node.platform.GetComponent<PlatformState>().Z -1;
        
        for (int x = 0; x < adjacent.GetLength(0) ; x++)
        {
            for (int z = 0; z < adjacent.GetLength(1); z++)
            {
                adjacent[x , z ] = grid.grid[startX +x, startZ + z];
            }
        }
        return adjacent;
    }
    

private Node GetRandomNode(List<Node> nodes)
    {
        var random = Random.Range(0, nodes.Count);
        var node = nodes[random];
        return node;
    }
    private List<Node> ReturnRandomPath()
    {
        var random = Random.Range(0, LevelPaths.Count);
        var path = LevelPaths[random];
        return path;
    }

    public GameObject[,] ReturnMap()
    {
        return _tileArray;
    }

    private void GetPath(List<List<Node>> nodes)
    {
        nodes.Add(pathFinding.ReturnPath());
        foreach (var go in nodes)
        {
            foreach (var node in go)
            {
                node.InUse = true;
           
            }
            
        }
    }
}
    


