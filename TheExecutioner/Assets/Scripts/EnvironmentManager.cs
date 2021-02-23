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
    public PlatformManager platformManager;
    private EnvironmentSpawner environmentSpawner;
    private Grid grid;

    private List<GameObject[,]> EntranceSpawnTile = new List<GameObject[,]>();
    private List<GameObject[,]> LevelWalls = new List<GameObject[,]>();
    private List<GameObject[,]> LevelRooms = new List<GameObject[,]>();
    private List<GameObject[,]> RandomPlatforms = new List<GameObject[,]>();
    private List<GameObject> Stairs = new List<GameObject>();
    private List<List<GameObject[,]>> LevelPlatforms = new List<List<GameObject[,]>>();
    public List<List<Node>> LevelPaths = new List<List<Node>>();

    public void ClearPaths()
    {
        LowerAllPathPlatforms();
        LevelPaths.Clear();
    }

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
        {
            navmeshSurface.BuildNavMesh();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
    
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            RaiseAllPathPlatforms();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SmoothMap();
        }

    }
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
    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
    }
    public void SpawnStairs()
    {
        List<Node> stairSpawns = new List<Node>();
        for (int i = 0; i < LevelPaths.Count; i++)
        {
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
        foreach (var go in path)
        {
            go.InUse = true;
        }
        return path;
    }

    public GameObject[,] ReturnMap()
    {
        return _tileArray;
    }

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
        
    }
}
    


