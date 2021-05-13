using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] public RoundManager roundManager;
    [SerializeField] public GameObject playerSpawnPoint;
    public SpawnPointManager SpawnPointManager;
    public LimbSpawner LimbSpawner;
    public ZombieManager ZombieManager;
    public EnvironmentManager EnvironmentManager;
    public Pathfinding Pathfinding;
    [field:SerializeField] private LevelManager LevelManager { get; set; }
    public EventManager EventManager;
    [field: SerializeField] public bool BuildMode { get; set; }
    [field: SerializeField] public bool PlayMode { get; set; }
    [field: SerializeField] public bool MenuMode { get; set; }
    [field: SerializeField]public float BuildSpeed { get; set; }

    public override void Awake()
    {
        base.Awake();
        roundManager = GetComponent<RoundManager>();
        EventManager = GetComponentInChildren<EventManager>();
        EnvironmentManager = GetComponentInChildren<EnvironmentManager>();
        ZombieManager = GetComponentInChildren<ZombieManager>();
        LimbSpawner = GetComponentInChildren<LimbSpawner>();
        Pathfinding = GetComponentInChildren<Pathfinding>();
        SpawnPointManager = GetComponentInChildren<SpawnPointManager>();
        
    }
    private void Start()
    {
        if (PlayMode)
        {
            Invoke("StartNewRound",1f);
            Invoke("SpawnWeapons", 5f);
        }
        if(BuildMode)
        {
            LoadBuildMode();
        }

        if (MenuMode)
        {
            InvokeRepeating("MainMenuSequence",1f,7f);
        }
    }
    /// <summary>
    /// Load a level and stage
    /// Gameplay is disabled
    /// </summary>
    private void LoadBuildMode()
    {
        LevelManager.LoadLevel();
        LoadStage();
    }
    /// <summary>
    /// Load a stage through the level manager
    /// </summary>
    private void LoadStage()
    {
        LevelManager.LoadStage();
    }
    /// <summary>
    /// Start the next round sequence at the end of the level
    /// Clear the current pickups
    /// Invoke a new round after 1 seconds
    /// Invoke Spawn Weapons after 5 seconds
    /// </summary>
    public void NextRoundSequence()
    {
        SpawnPointManager.ClearWeapons();
        Invoke("StartNewRound",1f);
        Invoke("SpawnWeapons", 5f);
    }
    /// <summary>
    /// Spawn Weapons
    /// </summary>
    private void SpawnWeapons()
    {
        for (int i = 0; i < 12; i++)
        {
            SpawnPointManager.SpawnWeapon(EnvironmentManager.EnemySpawnPoints.ReturnInternalSpawnPoint());
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
    /// <summary>
    /// Toggle gameover
    /// Disable the timescale
    /// Disable the game canvas
    /// Enable the death canvas
    /// Enable the cursor and confine it to the screen
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0;
        uiCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }
    /// <summary>
    /// Start a new round in the roundManager reference
    /// </summary>
    private void StartNewRound()
    {
        roundManager.StartNewRound();
    }

    private void MainMenuSequence()
    {
        LevelManager.LoadStage();
    }
}



