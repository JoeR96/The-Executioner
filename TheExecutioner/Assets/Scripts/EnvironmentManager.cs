using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;
using Random = UnityEngine.Random;



public class EnvironmentManager : MonoBehaviour
{

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
    
    public void RaiseWall()
    {
        SpawnPoints.Clear();
        foreach (var go in grid.grid)
        {
            if (go.PlatformState.PlatformIsWall)
            {
                go.PlatformState.SetPlatformHeight((int)PlatformHeight.RaisedFour);
                SpawnPoints.Add(go.PlatformState.spawnPoint);
                go.platform.layer = 13;
            }
            go.PlatformState.SetState();
        }

        for (int i = 10; i < 30; i++)
        {
            for (int j = 10; j < 30; j++)
            {
                grid.grid[i, j].PlatformState.SetPlatformHeight((int)PlatformHeight.Flat);
                grid.grid[i,j].platform.layer = 0;
            }
        }
        navmeshSurface.BuildNavMesh();
    }
    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
        navmeshSurfaceTwo.BuildNavMesh();
    }

    
    
    
    
    

    
}
    


