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
    private bool roundActive;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F7))
            StartNewRound();
        
        if(roundActive)
            CheckForRoundCompletion();
    }
    /// <summary>
    /// Start a new round
    /// Load a level
    /// Load an index of the new level
    /// Invoke NewRound
    /// Trigger the round active
    /// </summary>
    public void StartNewRound()
    {
        levelManager.LoadLevel();
        levelManager.LoadStage();
        StartCoroutine(NewRound());
        Invoke("TriggerBoolChange", 10f);
    }
    /// <summary>
    /// Increase enemy count
    /// Increase event spawn count
    /// Check if an event should be spawned
    /// Spawn an event if it current round is divisible by three
    /// Invoke spawn wave
    /// </summary>
    /// <returns></returns>
    public IEnumerator NewRound()
    {
        IncreaseZombieSpawnCount();
        SetNewRound();
        yield return new WaitForSeconds(3.25f);
        levelManager.BuildNavMesh();
        if(CurrentRound % 3 == 0)
            StartCoroutine(StartEvents());
        StartCoroutine(SpawnWave());
    }
    /// <summary>
    /// Increase the zombie spawn count value
    /// </summary>
    private void IncreaseZombieSpawnCount()
    {
        CurrentRoundZombieSpawnCount += 8;
    }
    /// <summary>
    /// Increase the event spawn count
    /// Increment the round counter
    /// </summary>
    private void SetNewRound()
    {
        currentEventSpawnCount =  (int) (currentEventSpawnCount * 1.25f);
        Mathf.RoundToInt(currentEventSpawnCount);
        CurrentRound++;
    }
    /// <summary>
    /// Instantiate current amount of events
    /// Delay 3 seconds between each event spawning
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartEvents()
    {
        for (int i = 0; i < currentEventSpawnCount; i++)
        {
            eventManager.PlayEvent();
            yield return new WaitForSeconds(3f);
        }
    }
    /// <summary>
    /// Spawn zombies
    /// Every 4 zombies that are spawned delay 4 seconds
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnWave()
    {
        for (int j = 0; j < CurrentRoundZombieSpawnCount; j++)
            {
                zombieSpawner.SpawnZombie();
                if (j % 4 == 0)
                {
                    yield return new WaitForSeconds(4f);
                }
            }
    }
    /// <summary>
    /// Return the active zombie count
    /// </summary>
    /// <returns></returns>
    public int ReturnActiveZombieCount()
    {
        return zombieSpawner.ActiveZombies.Count;
    }
    /// <summary>
    /// Check active zombie count
    /// return true if more can spawn
    /// </summary>
    /// <returns></returns>
    private bool SpawnNormalZombieCheck()
    {
        return zombieSpawner.ActiveZombies.Count == 20;
    }
    /// <summary>
    /// Returns a boolean dependant on any enemies being alive
    /// </summary>
    /// <returns></returns>
    private bool AllZombiesDead()
    {
        return zombieSpawner.ActiveZombies.Count == 0 && zombieSpawner.EliteZombie.Count == 0;
    }
    /// <summary>
    /// toggle the round to active
    /// this method is invoked
    /// </summary>
    private void TriggerBoolChange()
    {
        roundActive = true;
    }
    /// <summary>
    /// Return a boolean dependant on any events being active
    /// </summary>
    /// <returns></returns>
    private bool CheckEventStatus()
    {
        return eventManager.ActiveEvents.Count == 0;
    }
    /// <summary>
    /// Triggers the round over if all zombies are killed and all events are completed
    /// </summary>
    private void CheckForRoundCompletion()
    {
        if (AllZombiesDead() && CheckEventStatus())
        {
            GameManager.instance.NextRoundSequence();
            roundActive = false;
        }
            
    }
}
