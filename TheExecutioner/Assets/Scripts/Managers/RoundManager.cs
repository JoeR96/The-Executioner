using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private ZombieSpawner zombieSpawner;

    public int CurrentRound { get; set; }
    public int CurrentRoundZombieSpawnCount { get; set; }
    private int eventsToSpawn;

    private void Start()
    {
        CurrentRound = 1;
        CurrentRoundZombieSpawnCount = 12;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F7))
            StartNewRound();
    }

    public void StartNewRound()
    {
        IncreaseSpawnCount();
        SetNewRound();
        StartCoroutine(StartEvents());
        SpawnWave();
    }

    private void SetNewRound()
    {
        eventsToSpawn = CurrentRound / 2;
        Mathf.RoundToInt(eventsToSpawn);
        CurrentRound++;
    }

    private IEnumerator StartEvents()
    {
        for (int i = 0; i < eventsToSpawn; i++)
        {
            eventManager.PlayEvent();
        }
        yield return new WaitForSeconds(3f);
    }

    private void SpawnWave()
    {
        for (int i = 0; i < CurrentRoundZombieSpawnCount; i++)
        {
            zombieSpawner.SpawnZombie();
        }
    }
    
    private void IncreaseSpawnCount()
    {
        var count = CurrentRoundZombieSpawnCount *  1.25f;
        CurrentRoundZombieSpawnCount = Mathf.RoundToInt(count);
    }

    public int ReturnActiveZombieCount()
    {
        return zombieSpawner.ActiveZombies.Count;
    }
}
