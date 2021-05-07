using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZombieSpawner 
{
    /// <summary>
    /// By separating this class from monobehaviour a new instance of the class can be spawned with ease
    /// </summary>
    private ZombieSpawner zombieSpawner;
    private Transform targetPos;
    public int count;

    /// <summary>
    /// Use the gamemanager singleton to access the zombie spawner rather than using findobjectoftype
    /// Pass a target location in to the constructor to specify a target location for the AI director system
    /// Pass in a spawn count to specify how many zombies to spawn for the event
    /// </summary>
    /// <param name="spawnCount"></param>
    /// <param name="target"></param>
    public EventZombieSpawner(int spawnCount, Transform target)
    {
        zombieSpawner = GameManager.instance.ZombieManager.ZombieSpawner;
        targetPos = target;
        count = spawnCount;
    }
    /// <summary>
    /// Spawn the appropriate number of zombies for the event according to the count specified within the class instance
    /// Set the Zombie in to an event state
    /// </summary>
    public void SpawnZombiesTargetingEvent()
    {
        for (int i = 0; i < count; i++)
        {
            var zombie = zombieSpawner.SpawnZombie();
            SetZombieState(zombie);
        }
    }
    /// <summary>
    /// Reference the AiAgent component on the Zombie
    /// Set the event target location to the target position defined in the constructor
    /// Change the StateMachine state to the event state now it has the required information to enter
    /// </summary>
    /// <param name="zombie"></param>
    private void SetZombieState(GameObject zombie)
    {
        var aiAgent = zombie.GetComponent<AiAgent>();
        aiAgent.EventTarget = targetPos;
        aiAgent.StateMachine.ChangeState(StateId.EventState);
    }
}
