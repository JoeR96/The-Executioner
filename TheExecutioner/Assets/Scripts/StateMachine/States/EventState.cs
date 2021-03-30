using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EventState : IState
{
    private Transform eventTarget;
    private float _timer = 00f;
    public StateId GetId()
    {
        return StateId.EventState;
    }

    public void Enter(AiAgent agent)
    {
        eventTarget = GameManager.instance.EventManager.ReturnActiveRandomEventTransform();
        Debug.Log(eventTarget);
        agent.navMeshAgent.destination = eventTarget.position;
    }

    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.destination = eventTarget.position;
        if (IsAgentOnNavMesh(agent) && agent.navMeshAgent.isOnNavMesh)
        {
            if(!agent.enabled )
                return;
            
            _timer -= Time.deltaTime;
            if (!agent.navMeshAgent.hasPath)
            {
                agent.navMeshAgent.destination = eventTarget.position;
            }

            if (_timer < 0.0f)
            {
                Vector3 direction = (eventTarget.position - agent.navMeshAgent.destination);
                direction.y = 0;
                if (direction.sqrMagnitude > agent.AgentConfig.MaxDistance * agent.AgentConfig.MaxDistance) 
                {
                    if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                    {
                        agent.navMeshAgent.destination = eventTarget.position;
                    }
                }
                _timer = agent.AgentConfig.MaxTime;
            }
        }

        // if (Vector3.Distance(agent.transform.position, eventTarget.transform.position) <=1.25f)
        // {
        //     //agent.StateMachine.ChangeState(StateId.Attack);
        // }
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
