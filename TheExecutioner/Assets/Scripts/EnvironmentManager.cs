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

    
    public Transform NavMeshObject;
    private GameObject[,] _tileArray;
    private WallManager wallManager;
    private RoomManager roomManager;
    public Pathfinding pathFinding;
    private NavMeshSurface navmeshSurface;
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
        for (int i = 0; i < 23; i++)
        {
            grid.grid[6+ i,28].PlatformState.SetPlatformHeight((int)PlatformHeight.RaisedTwice);
            grid.grid[28, 6+ i].PlatformState.SetPlatformHeight((int)PlatformHeight.RaisedTwice);
            grid.grid[6+ i,6].PlatformState.SetPlatformHeight((int)PlatformHeight.RaisedTwice);
            grid.grid[6, 6+ i].PlatformState.SetPlatformHeight((int)PlatformHeight.RaisedTwice);
        }
    }
    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
    }

    

    
    
    
    
    

    
}
    


