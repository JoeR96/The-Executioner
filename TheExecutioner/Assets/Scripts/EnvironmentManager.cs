using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using eDmitriyAssets.NavmeshLinksGenerator;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;
using Random = UnityEngine.Random;



public class EnvironmentManager : MonoBehaviour
{

    public bool InversePlatforms = false
        ;
    public List<GameObject> SpawnPoints = new List<GameObject>();
    public Transform NavMeshObject;
    public Transform NavMeshObjectTwo;

    
    private GameObject[,] _tileArray;
    private WallManager wallManager;
    private RoomManager roomManager;
    public Pathfinding pathFinding;
    private NavMeshSurface navmeshSurface;
    private NavMeshSurface navmeshSurfaceTwo;
    public PlatformManager PlatformManager;
    public EnvironmentSpawner environmentSpawner;
    public NavMeshLinks_AutoPlacer navMeshLinkGenerator;
    
        public Grid grid;

    public List<Node[,]> Levels = new List<Node[,]>();



    public void LowerAll()
    {
        foreach (var node in grid.grid)
        {
            node.PlatformState.SetPlatformHeight((int)PlatformHeight.Flat);
            if (node.PlatformState.stairs.GetComponent<MeshRenderer>().enabled)
            {
                
                node.PlatformState.PlatformStairActive = false;
                node.PlatformState.ActivateStairs(false);
            }
            
        }

    }
    private void Awake()
    {

        environmentSpawner = GetComponent<EnvironmentSpawner>();
        pathFinding = GetComponent<Pathfinding>();
        navmeshSurface = NavMeshObject.GetComponent<NavMeshSurface>();
        navmeshSurfaceTwo = NavMeshObjectTwo.GetComponent<NavMeshSurface>();
        wallManager = GetComponent<WallManager>();
        roomManager = GetComponent<RoomManager>();
        PlatformManager = GetComponent<PlatformManager>();

        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F12))
        {
            BuildNavMesh();
        }
    }
    private void Start()
    {
        
    }
    public void StartBunkers()
    {
        environmentSpawner.SpawnBunkers(environmentSpawner.LevelBunkers);
    }
    public void StartHighBunkers()
    {
       environmentSpawner.SpawnHighBunkers(environmentSpawner.LevelHighBunkers);
    }

    public void StartLowBunkers()
    {
        environmentSpawner.SpawnLowBunkers(environmentSpawner.LevelLowBunkers);
    }
    public void RaiseWallTwo()
    {
        //The purpose of setting the layer is to seperate the arena navmesh from the navmesh surface above
        //Using the walls that are always raised around the pit as a seperate navmesh surface seemed the best solution
        
        SpawnPoints.Clear();
        
        //Loop through the corresponding tiles to set the playable arena tiles
        for (int i = 10; i < 30; i++)
        {
            for (int j = 10; j < 30; j++)
            {
                var gridPosition = grid.grid[i, j];
                gridPosition.PlatformState.PlatformIsWall = false;
            }
        }
        
        foreach (var node in grid.grid)
        {
            if (node.PlatformState.PlatformIsWall)
            {
                node.PlatformState.SetPlatformHeight((int)PlatformHeight.LoweredSix);
                SpawnPoints.Add(node.PlatformState.spawnPoint);
  
            }
            node.PlatformState.SetState();
        }
        
        navmeshSurface.BuildNavMesh();
        //var t = GetComponent<OffMeshLink>();
    }
    public void RaiseWall()
    {
        //The purpose of setting the layer is to seperate the arena navmesh from the navmesh surface above
        //Using the walls that are always raised around the pit as a seperate navmesh surface seemed the best solution
        
        SpawnPoints.Clear();
        
        //Loop through the corresponding tiles to set the playable arena tiles
        for (int i = 10; i < 30; i++)
        {
            for (int j = 10; j < 30; j++)
            {
                var gridPosition = grid.grid[i, j];
                gridPosition.PlatformState.PlatformIsWall = false;
            }
        }
        
        foreach (var node in grid.grid)
        {
            if (node.PlatformState.PlatformIsWall)
            {
                node.PlatformState.SetPlatformHeight((int)PlatformHeight.RaisedSix);
                SpawnPoints.Add(node.PlatformState.spawnPoint);
                
            }
            node.PlatformState.SetState();
        }
        
        navmeshSurface.BuildNavMesh();
    }
    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
        navmeshSurfaceTwo.BuildNavMesh();
    }
    
    
    
    
    

    
}
    


