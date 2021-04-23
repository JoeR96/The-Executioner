using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private ZombieSpawner zombieSpawner;
    [SerializeField] private LevelManager levelManager;

    public int CurrentRound { get; set; }
    public int CurrentRoundZombieSpawnCount { get; set; }
    public int CurrentRoundZombieEliteSpawnCount { get; set; }
    private int currentEventSpawnCount;
    private bool roundActive = false;

    private void Start()
    {
        CurrentRoundZombieSpawnCount = 0;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F7))
            StartNewRound();
        
        if(roundActive)
            CheckForRoundCompletion();
    }



    public void StartNewRound()
    {
        StartCoroutine(NewRound());
    }
    public IEnumerator NewRound()
    {
        levelManager.LoadLevel();
        levelManager.LoadStage();
        IncreaseZombieSpawnCount();
        
        SetNewRound();
        yield return new WaitForSeconds(3.25f);
        levelManager.BuildNavMesh();
        if(CurrentRound % 3 == 0)
            StartCoroutine(StartEvents());
        StartCoroutine(SpawnWave());
    }

    private void IncreaseZombieSpawnCount()
    {
        CurrentRoundZombieSpawnCount += 12;
    }
    private void SetNewRound()
    {
        currentEventSpawnCount = (int) (currentEventSpawnCount * 1.25f);
        Mathf.RoundToInt(currentEventSpawnCount);
        CurrentRound++;
    }

    private IEnumerator StartEvents()
    {
        for (int i = 0; i < currentEventSpawnCount; i++)
        {
            eventManager.PlayEvent();
        }

        IncreaseEventSpawnCount();
        yield return new WaitForSeconds(3f);
    }

    private void IncreaseEventSpawnCount()
    {
        
    }

    private void InitiateWaveSpawn()
    {
        
    }
    private IEnumerator SpawnWave()
    {
        Debug.Log(CurrentRound);
        Debug.Log(CurrentRoundZombieSpawnCount);

        roundActive = true;

            for (int j = 0; j < CurrentRoundZombieSpawnCount; j++)
            {
                zombieSpawner.SpawnZombie();
                if (j % 4 == 0)
                {
                    yield return new WaitForSeconds(4f);
                }
            }
    }
    
    public int ReturnActiveZombieCount()
    {
        return zombieSpawner.ActiveZombies.Count;
    }

    private bool SpawnNormalZombieCheck()
    {
        return zombieSpawner.ActiveZombies.Count == 20;
    }

    private bool AllZombiesDead()
    {
        return zombieSpawner.ActiveZombies.Count == 0 && zombieSpawner.EliteZombie.Count == 0;
    }

    private bool CheckEventStatus()
    {
        return eventManager.ActiveEvents.Count == 0;
    }
    private void CheckForRoundCompletion()
    {
        if (AllZombiesDead() && CheckEventStatus())
        {
            GameManager.instance.NextRoundSequence();
            roundActive = false;
        }
            
    }
}
