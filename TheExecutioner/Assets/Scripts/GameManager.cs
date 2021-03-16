using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = System.Random;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public ZombieOverflowEvent ZombieOverFlowEvent;
    public LimbSpawner LimbSpawner;
    public ZombieManager ZombieManager;
    public EnvironmentManager EnvironmentManager;
    public Pathfinding pathfinding;
    public LevelManager LevelManager;
    public Grid Grid;

    public override void Awake()
    {
        base.Awake();
        EnvironmentManager = GetComponentInChildren<EnvironmentManager>();
        ZombieOverFlowEvent = GetComponentInChildren<ZombieOverflowEvent>();
        ZombieManager = GetComponentInChildren<ZombieManager>();
        LimbSpawner = GetComponentInChildren<LimbSpawner>();
        pathfinding = GetComponentInChildren<Pathfinding>();
        Grid = GetComponentInChildren<Grid>();
        LevelManager = GetComponentInChildren<LevelManager>();
        
    }

    private void Start()
    {
        
    }
    private void StartGame()
    {
        EnvironmentManager.BuildNavMesh();
        EnvironmentManager.navMeshLinkGenerator.Generate();
        
    }
    private void StartGameSequence()
    {
        
        Invoke("StartLevel", 0.25f);
        Invoke("StartGame", 2f);
        Invoke("SpawnWeapons" ,3f);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ZombieManager.ZombieSpawner.SpawnActiveZombiesAtLocation(EnvironmentManager.EnemySpawnPoints.internalSpawnPoints);
        }
 
    }

    
    
}



