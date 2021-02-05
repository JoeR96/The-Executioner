﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float MaxTime = 1.0f;
    public float MaxDistance = 1.0f;
    public float DieForce = 10.0f;
    public float maxsightDistance = 5.0f;
}