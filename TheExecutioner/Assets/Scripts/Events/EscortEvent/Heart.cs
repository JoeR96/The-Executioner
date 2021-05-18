using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Heart : MonoBehaviour
{
    [SerializeField] private HeartEscortEvent heartEscortEvent;
    private NavMeshAgent navmeshAgent;
    public Transform Destination;
    public bool EventComplete { get; set; }

    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        navmeshAgent.speed = Random.Range(1.5f, 2f);
    }

    public void SetTargetPosition(Transform altarLocation)
    {
        
        navmeshAgent.destination = altarLocation.position;
        Destination = altarLocation;
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
