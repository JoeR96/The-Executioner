﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject gameCanvas;
    
    public InteractionSpawnPointManager InteractionSpawnPointManager;
    public LimbSpawner LimbSpawner;
    public ZombieManager ZombieManager;
    public EnvironmentManager EnvironmentManager;
    public Pathfinding Pathfinding;
    public LevelManager LevelManager;

    public EventManager EventManager;
    public bool BuildMode;
 
    public override void Awake()
    {
        base.Awake();
        EventManager = GetComponentInChildren<EventManager>();
        EnvironmentManager = GetComponentInChildren<EnvironmentManager>();
        ZombieManager = GetComponentInChildren<ZombieManager>();
        LimbSpawner = GetComponentInChildren<LimbSpawner>();
        Pathfinding = GetComponentInChildren<Pathfinding>();
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
        var random = Random.Range(0, 3);
        LevelManager.LoadLevel(random);

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
        List<Transform> newList = EnvironmentManager.EnemySpawnPoints.InternalSpawnPoints;
        List<Transform> temp = new List<Transform>();
        
        int length = newList.Count;
        int newLength = length / 25;

        for (int i = 0; i < newLength; i++)
        {
            int random = Random.Range(0, newList.Count);
            Transform spawnPoint = newList[random];
            
            newList.RemoveAt(random);
            temp.Add(spawnPoint);
        }
        
        ZombieManager.ZombieSpawner.SpawnRagdollZombiesAtLocations(temp);
    }
    private void SpawnArmoredZombies()
    {
        List<Transform> newList = EnvironmentManager.EnemySpawnPoints.InternalSpawnPoints;
        List<Transform> temp = new List<Transform>();
        
        int length = newList.Count;
        int newLength = length / 75;

        for (int i = 0; i < newLength; i++)
        {
            int random = Random.Range(0, newList.Count);
            Transform spawnPoint = newList[random];
            
            newList.RemoveAt(random);
            temp.Add(spawnPoint);
        }
        
        ZombieManager.ZombieSpawner.SpawnArmoredZombiesAtLocations(temp);
    }

    public void RagdollAllDuringLevelChange()
    {
        
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
        if (Input.GetKeyDown(KeyCode.F2))
        {
           EventManager.PlaySacrificeEvent();
           EventManager.AssignEvents();
        }
        if(Input.GetKeyDown(KeyCode.F3))
            SpawnZombies();
        
        if(Input.GetKeyDown(KeyCode.F4))
            SpawnArmoredZombies();
        
        if(Input.GetKeyDown(KeyCode.F5))
            EventManager.PlayHeartEscortEvent();

    }
    
    public void GameOver()
    {
        uiCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
}



