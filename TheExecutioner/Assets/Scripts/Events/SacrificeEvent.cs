﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeEvent : Event 
{
    private int currentKillCount;
    private int targetKillCount;
    private int limbKillCount;
    private int limbBonusTargetCount;

    private void OnEnable()
    {
        StartEvent();
    }

}


