using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = System.Random;

public class GameManager : Singleton<GameManager>
{
    public InteractionSpawnPointManager InteractionSpawnPointManager;
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
        ZombieManager = GetComponentInChildren<ZombieManager>();
        LimbSpawner = GetComponentInChildren<LimbSpawner>();
        pathfinding = GetComponentInChildren<Pathfinding>();
        Grid = GetComponentInChildren<Grid>();
        LevelManager = GetComponentInChildren<LevelManager>();
        InteractionSpawnPointManager = GetComponent<InteractionSpawnPointManager>();
    }
    
    private void StartGame()
    {
        EnvironmentManager.BuildNavMesh();
        EnvironmentManager.navMeshLinkGenerator.Generate();
        for (int i = 0; i < 3; i++)
        {
            InteractionSpawnPointManager.SpawnWeapon(EnvironmentManager.EnemySpawnPoints.ReturnInternalSpawnPoint());
        }
        
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
            InteractionSpawnPointManager.SpawnWeapon(EnvironmentManager.EnemySpawnPoints.ReturnInternalSpawnPoint());
        }
 
    }

    
    
}



