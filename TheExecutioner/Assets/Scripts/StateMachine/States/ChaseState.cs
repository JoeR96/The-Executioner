﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private float timer = 1f;
    private float attackRange = 1.25f;
    public StateId GetId()
    {
        return StateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        
    }

    public void Update(AiAgent agent)
    {
        if (IsAgentOnNavMesh(agent) && agent.navMeshAgent.isOnNavMesh)
        {
            if(!agent.enabled )
                return;
            
            timer -= Time.deltaTime;
            if (!agent.navMeshAgent.hasPath)
                agent.navMeshAgent.destination = agent.Player.position;

            if (timer < 0.0f)
            {
                Vector3 direction = (agent.Player.position - agent.navMeshAgent.destination);
                direction.y = 0;
                if (direction.sqrMagnitude > agent.AgentConfig.MaxDistance * agent.AgentConfig.MaxDistance)
                {
                    if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                        agent.navMeshAgent.destination = agent.Player.position;
                }
                    if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                    {
                        agent.navMeshAgent.destination = agent.Player.position;

                    }

                timer = agent.AgentConfig.MaxTime;
            }
        
        }

        if (Vector3.Distance(agent.transform.position, agent.Player.transform.position) <= attackRange)
        {
            agent.StateMachine.ChangeState(StateId.Attack);
        }
        if (Vector3.Distance(agent.transform.position, agent.Player.transform.position) >= attackRange)
        {
            agent.StateMachine.ChangeState(StateId.ChasePlayer);
        }
    }

    public bool IsAgentOnNavMesh(AiAgent agent)
    {
        float onMeshThreshold = 3;
        Vector3 agentPosition = agent.transform.position;
        NavMeshHit hit;

        // Check for nearest point on navmesh to agent, within onMeshThreshold
        if (NavMesh.SamplePosition(agentPosition, out hit, onMeshThreshold, NavMesh.AllAreas))
        {
            // Check if the positions are vertically aligned
            if (Mathf.Approximately(agentPosition.x, hit.position.x)
                && Mathf.Approximately(agentPosition.z, hit.position.z))
            {
                // Lastly, check if object is below navmesh
                return agentPosition.y >= hit.position.y;
            }
        }

        return false;
    }
    public void Exit(AiAgent agent)
    {
 
    }
}
