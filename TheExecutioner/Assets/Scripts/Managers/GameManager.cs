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
    
    public InteractionSpawnPointManager InteractionSpawnPointManager;
    public ZombieOverflowEvent ZombieOverFlowEvent;
    public LimbSpawner LimbSpawner;
    public ZombieManager ZombieManager;
    public EnvironmentManager EnvironmentManager;
    public Pathfinding pathfinding;
    public LevelManager LevelManager;
    public Grid Grid;
    public EventManager EventManager;
    public bool BuildMode;
    public int SpawnDivider;
    public override void Awake()
    {
        base.Awake();
        EventManager = GetComponentInChildren<EventManager>();
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
        var random = Random.Range(0, 4);
        LevelManager.LoadLevel(random);

    }
    private void StartGameSequence()
    {
        
        Invoke("LoadLevel", 0.25f);
        Invoke("StartGame", 6f);
        Invoke("SpawnWeapons" ,6f);
        Invoke("SpawnZombies",7f);
    }

    public void AssignEvents()
    {
        foreach (var go in ZombieManager.ZombieSpawner.ActiveFodderZombies)
        {
            var x = go.GetComponent<AiAgent>();
            x.StateMachine.ChangeState(StateId.EventState);
        }
        foreach (var go in ZombieManager.ZombieSpawner.ArmoredZombies)
        {
            var x = go.GetComponent<AiAgent>();
            x.StateMachine.ChangeState(StateId.EventState);
        }
    }
    private void SpawnZombies()
    {
        List<Transform> newList = EnvironmentManager.EnemySpawnPoints.internalSpawnPoints;
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
        List<Transform> newList = EnvironmentManager.EnemySpawnPoints.internalSpawnPoints;
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
           AssignEvents();
        }
        if(Input.GetKeyDown(KeyCode.F3))
            SpawnZombies();
        
        if(Input.GetKeyDown(KeyCode.F4))
            SpawnArmoredZombies();

    }
    
    public void GameOver()
    {
        uiCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
}



