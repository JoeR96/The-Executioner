﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Heart : MonoBehaviour
{
    [SerializeField] private HeartEscortEvent heartEscortEvent;
    private NavMeshAgent navmeshAgent;
    public Transform Destination;
    private bool eventComplete = false;
    
    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        navmeshAgent.speed = Random.Range(1.5f, 3f);
    }
    
    private void Update()
    {
        if (HasReachedDestination() && eventComplete != true)
        {
            eventComplete = true;
            heartEscortEvent.EventComplete(this);
        }
    }
    public void SetTargetPosition(Transform altarLocation)
    {
        
        navmeshAgent.destination = altarLocation.position;
        Destination = altarLocation;
    }

    public void StartEvent(Transform target)
    {
        EnableNavMeshAgent();
        SetTargetPosition(target);
    }
    public bool HasReachedDestination()
    {
        if (!navmeshAgent.pathPending)
        {
            if (navmeshAgent.remainingDistance <= navmeshAgent.stoppingDistance)
            {
                if (!navmeshAgent.hasPath || navmeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void DisableNavmeshAgent()
    {
        navmeshAgent.enabled = false;
    }
    
    public void EnableNavMeshAgent()
    {
        navmeshAgent.enabled = true;
    }
}
