using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTargetKillCount 
{
    public int TargetKillCount { get; set; }
    public int CurrentKillCount { get; set; }
    
    public EventTargetKillCount(int targetKillCount)
    {
        TargetKillCount = targetKillCount;
        CurrentKillCount = 0;
    }

    public void IncreaseKillCount()
    {
        CurrentKillCount++;
    }
}
