using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;


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
    public bool BuildMode;
    public override void Awake()
    {
        base.Awake();
        EnvironmentManager = GetComponentInChildren<EnvironmentManager>();
        ZombieManager = GetComponentInChildren<ZombieManager>();
        LimbSpawner = GetComponentInChildren<LimbSpawner>();
        pathfinding = GetComponentInChildren<Pathfinding>();
        Grid = GetComponentInChildren<Grid>();
        LevelManager = GetComponentInChildren<LevelManager>();
        InteractionSpawnPointManager = GetComponentInChildren<InteractionSpawnPointManager>();
    }

    private void Start()
    {
        if (!BuildMode)
        {
            InvokeRepeating("StartGameSequence", 1, 45);
        }
        else
        {
            LoadLevel();
        }
        
    }
    private void StartGame()
    {
        EnvironmentManager.BuildNavMesh();
    }

    private void LoadLevel()
    {
        var random = Random.Range(0, 2);
        Debug.Log(random);
        Debug.Log(LevelManager.name);
        LevelManager.LoadLevel(random);
        Debug.Log(LevelManager);
    }
    private void StartGameSequence()
    {
        
        Invoke("LoadLevel", 0.25f);
        Invoke("StartGame", 6f);
        Invoke("SpawnWeapons" ,6f);
        Invoke("SpawnZombies",7f);
    }

    private void SpawnZombies()
    {
        ZombieManager.ZombieSpawner.SpawnActiveZombiesAtLocation(EnvironmentManager.EnemySpawnPoints.internalSpawnPoints);
    }
    private void SpawnWeapons()
    {
        for (int i = 0; i < 5; i++)
        {
            InteractionSpawnPointManager.SpawnWeapon(EnvironmentManager.EnemySpawnPoints.ReturnInternalSpawnPoint());
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SpawnWeapons();
        }
 
    }

    
    
}



