using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeEvent : Event 
{
    public override void Awake()
    {
        waveSpawnTotal = 4 * GameManager.instance.roundManager.CurrentRound + 1;
        eventZombieSpawner = new EventZombieSpawner(waveSpawnTotal,transform);
        base.Awake();
    }
    protected override void Start()
    {
        var round = GameManager.instance.roundManager.CurrentRound;
        EventTargetKillCountMultiplier = 3 * round;
        base.Start();
        waveSpawnTotal = 4 * round;
        StartEvent();
        eventZombieSpawner.SpawnZombies();
    }

    private void Update()
    {
        if(EventTargetKillCountManager.ReturnKillCountComplete() && eventComplete == false && isActiveAndEnabled)
        {
            eventComplete = true;
            EventComplete();
        }
    }
}


