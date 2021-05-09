using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeEvent : Event 
{
    private int currentKillCount;
    private int limbKillCount;
    private int limbBonusTargetCount;
    [SerializeField] private ParticleSystem Fog;
    

    private void OnEnable()
    {
        EventTargetKillCountMultiplier = 5;
        waveSpawnTotal = 10;
        StartEvent();
        Fog.Play();
    }
    

}


