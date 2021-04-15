using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private RoundManager roundManager;
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
        roundManager = GetComponent<RoundManager>();
    }

    private void Start()
    {
        if (!BuildMode)
        {
            NextRoundSequence();
        }
        else
        {
            LoadStage();
        }
        
    }
    private void BuildNavMesh()
    {
        EnvironmentManager.BuildNavMesh();
    }

    private void LoadStage()
    {
        var random = Random.Range(0, 3);
        LevelManager.LoadLevel(0);

    }
    private void NextRoundSequence()
    {
        Invoke("LoadStage",0.25f);
        Invoke("BuildNavMesh", 6f);
        Invoke("StartNewRound",7f);
    }

    private void StartNewRound()
    {
        roundManager.StartNewRound();
    }
    private void SpawnFodderZombies()
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
           
        }
        if(Input.GetKeyDown(KeyCode.F3))
            SpawnFodderZombies();
        
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



