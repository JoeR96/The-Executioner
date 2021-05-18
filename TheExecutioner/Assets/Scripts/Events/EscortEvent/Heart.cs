using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Heart : MonoBehaviour
{
    [SerializeField] private HeartEscortEvent heartEscortEvent;
    private NavMeshAgent navmeshAgent;
    private Transform destination;
    public bool EventComplete { get; set; }

    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
    }
    /// <summary>
    /// Set the target position to move to
    /// </summary>
    /// <param name="altarLocation"></param>
    public void SetTargetPosition(Transform altarLocation)
    {
        navmeshAgent.destination = altarLocation.position;
        destination = altarLocation;
    }
    
    /// <summary>
    /// Enable the navmesh agent
    /// </summary>
    public void DisableNavmeshAgent()
    {
        navmeshAgent.enabled = false;
    }
    /// <summary>
    /// Disable the navmesh agent
    /// </summary>
    public void EnableNavMeshAgent()
    {
        navmeshAgent.enabled = true;
    }
}
