﻿using System;
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
    public EnvironmentSpawner environmentSpawner;
    public NavMeshLinks_AutoPlacer navMeshLinkGenerator;
    public EnemySpawnPoints EnemySpawnPoints;
    public Grid grid;
    
    private GameObject[,] _tileArray;
    private NavMeshSurface navmeshSurface;
    
    private void Awake()
    {
        EnemySpawnPoints = GetComponent<EnemySpawnPoints>();
        environmentSpawner = GetComponent<EnvironmentSpawner>();
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
        int platformHeight;
        
        SpawnPoints.Clear();
        //Loop through the corresponding tiles to set the playable arena tiles
        for (int i = 15; i < 25; i++)
        {
            for (int j = 15; j < 25; j++)
            {
                var gridPosition = grid.grid[i, j];
                gridPosition.PlatformManager.PlatformStateManager.PlatformIsWall = true;
            }
        }
        
        if (raiseUp)
            platformHeight = 15;

        else
            platformHeight = 14;
        
        foreach (var node in grid.grid)
        {
            if (!node.PlatformManager.PlatformStateManager.PlatformIsWall)
            {
                node.PlatformManager.PlatformHeightManager.SetPlatformHeight(platformHeight);
                EnemySpawnPoints.AddExternalSpawnPointToList(node);
            }
            EnemySpawnPoints.CheckForSpawnPoint(node);
        }
        
        navmeshSurface.BuildNavMesh();
    }


    public void LowerAll()
    {
        foreach (var node in grid.grid)
        {
            node.PlatformManager.PlatformHeightManager.SetPlatformHeight((int)PlatformHeight.Flat);
            if (node.PlatformManager.PlatformRampManager.ramp.GetComponent<MeshRenderer>().enabled)
            {
                
                node.PlatformManager.PlatformRampManager.PlatformRampActive = false;
                node.PlatformManager.PlatformRampManager.ActivateRamp(false);
            }
            
        }

    }
    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
    }
    
    
    
    
    

    
}
    


