﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateId
{
ChasePlayer,
DeathState,
Idle,
Attack,
Target,
EventState,
EventDeathState
}

public interface IState 
{
    StateId GetId();

    void Enter(AiAgent agent);

    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
