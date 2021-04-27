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
    [SerializeField] public GameObject playerSpawnPoint;
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
   
    private void LoadStage()
    {
        
        LevelManager.LoadStage();

    }
    public void NextRoundSequence()
    {

        InteractionSpawnPointManager.ClearWeapons();
        Invoke("StartNewRound",1f);
        Invoke("SpawnWeapons", 5f);
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
        for (int i = 0; i < 12; i++)
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

        if(Input.GetKeyDown(KeyCode.F5))
            EventManager.PlayHeartEscortEvent();
        
        if(Input.GetKeyDown(KeyCode.F6))
            NextRoundSequence();

    }
    
    public void GameOver()
    {
        Time.timeScale = 0;
        uiCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
}



