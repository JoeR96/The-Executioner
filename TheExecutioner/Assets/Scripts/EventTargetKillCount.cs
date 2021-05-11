using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTargetKillCount 
{
    public int TargetKillCount { get; set; }
    public int CurrentKillCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="multiplierValue"></param>
    /// <param name="waveNumber"></param>
    public EventTargetKillCount(int multiplierValue,int waveNumber)
    {
        TargetKillCount = waveNumber * multiplierValue;
        CurrentKillCount = 0;
    }

    public void IncreaseKillCount()
    {
        CurrentKillCount++;
    }
}
