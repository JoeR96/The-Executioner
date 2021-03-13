using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = System.Random;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] 
    public SpawnPointManager SpawnPointManager;
    public ZombieOverflowEvent ZombieOverFlowEvent;
    public LimbSpawner LimbSpawner;
    public ZombieSpawner ZombieSpawner;
    public EnvironmentManager EnvironmentManager;
    public Pathfinding pathfinding;
    public LevelManager LevelManager;
    public Grid Grid;

    public override void Awake()
    {
        base.Awake();
        EnvironmentManager = GetComponentInChildren<EnvironmentManager>();
        ZombieOverFlowEvent = GetComponentInChildren<ZombieOverflowEvent>();
        ZombieSpawner = GetComponentInChildren<ZombieSpawner>();
        LimbSpawner = GetComponentInChildren<LimbSpawner>();
        pathfinding = GetComponentInChildren<Pathfinding>();
        Grid = GetComponentInChildren<Grid>();
        LevelManager = GetComponentInChildren<LevelManager>();
        SpawnPointManager = GetComponentInChildren<SpawnPointManager>();
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
            StartLevel();
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            ZombieOverFlowEvent.PlayOverFlowEvent();
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            ZombieOverFlowEvent.PlayJailBreakEvent();
        }
    }

    private void StartLevel()
    {
        LevelManager.LoadStage(1);
    }

    public void SpawnWeapons()
    {
        var spawnPoints = LevelManager.ReturnSpawnPoints();
        foreach (var platform in spawnPoints)
        {
            SpawnPointManager.SpawnWeapon(platform.spawnPoint);
            platform.SetAdjacentColour(UnityEngine.Random.Range(6,11));
        }
        
    }


    
}



