using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private ZombieSpawner zombieSpawner;
    
    private int currentRound = 1;
    private int currentRoundZombieSpawnCount = 12;
    private int eventsToSpawn;
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
        SpawnZombies();
    }

    private void SetNewRound()
    {
        eventsToSpawn = currentRound / 2;
        Mathf.RoundToInt(eventsToSpawn);
        currentRound++;
    }

    private IEnumerator StartEvents()
    {
        for (int i = 0; i < currentRound; i++)
        {
            eventManager.PlayEvent();
        }
        yield return new WaitForSeconds(3f);
    }

    private void SpawnZombies()
    {
        for (int i = 0; i < currentRoundZombieSpawnCount; i++)
        {
            zombieSpawner.SpawnZombie();
        }
    }
    
    private void IncreaseSpawnCount()
    {
        var count = currentRoundZombieSpawnCount *  1.25f;
        currentRoundZombieSpawnCount = Mathf.RoundToInt(count);
    }
}
