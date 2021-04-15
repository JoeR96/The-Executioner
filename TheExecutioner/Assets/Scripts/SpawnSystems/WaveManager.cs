using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private ZombieSpawner zombieSpawner;
    [SerializeField] private int spawnCount;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F9))
            IncreaseSpawnCount();
        if(Input.GetKeyDown(KeyCode.F8))
            SpawnWave();
    }
    private void IncreaseSpawnCount()
    {
        var count = spawnCount *  1.25f;
        spawnCount = Mathf.RoundToInt(count);
    }

    private void SpawnWave()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            zombieSpawner.SpawnZombie();
        }
    }
    private void SetWaveEnemyTypes()
    {
        
    }
}
