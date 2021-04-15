﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventZombieSpawner 
{
    private ZombieSpawner zombieSpawner;
    private Transform targetPos;
    public int count;

    public EventZombieSpawner(int spawnCount, Transform target)
    {
        zombieSpawner = GameManager.instance.ZombieManager.ZombieSpawner;
        targetPos = target;
        count = spawnCount;
    }
    public void SpawnZombiesTargetingEvent()
    {
        for (int i = 0; i < count; i++)
        {
            var zombie = zombieSpawner.SpawnZombie();
            SetZombieState(zombie);
        }
    }

    private void SetZombieState(GameObject zombie)
    {
        var aiAgent = zombie.GetComponent<AiAgent>();
        aiAgent.EventTarget = targetPos;
        aiAgent.StateMachine.ChangeState(StateId.EventState);
    }
}