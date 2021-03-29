using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Heart : MonoBehaviour
{
    private NavMeshAgent navmeshAgent;
    public Transform Destination;
    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetTargetPosition(Transform altarLocation)
    {
        navmeshAgent.destination = altarLocation.position;
        Destination = altarLocation;
    }
}
