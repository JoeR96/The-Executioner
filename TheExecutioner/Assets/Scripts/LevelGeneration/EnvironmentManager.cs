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
    public List<GameObject> SpawnPoints = new List<GameObject>();
    public Transform NavMeshObject;
    public Transform NavMeshObjectTwo;
    private GameObject[,] _tileArray;
    public Pathfinding pathFinding;
    private NavMeshSurface navmeshSurface;
    public EnvironmentSpawner environmentSpawner;
    public NavMeshLinks_AutoPlacer navMeshLinkGenerator;
    public Grid grid;
    
    
    private void Awake()
    {
        environmentSpawner = GetComponent<EnvironmentSpawner>();
        pathFinding = GetComponent<Pathfinding>();
        navmeshSurface = NavMeshObject.GetComponent<NavMeshSurface>();
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F12))
        {
            BuildNavMesh();
        }
    }

    
    public void RaiseWall(bool raiseUp)
    {
        PlatformHeight platformHeight;
        
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

        
        if (raiseUp)
            platformHeight = PlatformHeight.RaisedOuterWall;

        else
            platformHeight = PlatformHeight.LoweredOuterWall;
        
        foreach (var node in grid.grid)
        {
            if (node.PlatformState.PlatformIsWall)
            {
                node.PlatformState.SetPlatformHeight((int)platformHeight);
                SpawnPoints.Add(node.PlatformState.spawnPoint);
                
            }
            node.PlatformState.SetState();
        }
        
        navmeshSurface.BuildNavMesh();
    }
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
    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
    }
    
    
    
    
    

    
}
    


