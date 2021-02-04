using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    public Transform PlayerTransform;

    private float _timer = 00f;
    
    public StateId GetId()
    {
        return StateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        if(PlayerTransform == null)
            PlayerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void Update(AiAgent agent)
    {
        if(!agent.enabled)
            return;
        _timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = PlayerTransform.position;
        }

        if (_timer < 0.0f)
        {
            Vector3 direction = (PlayerTransform.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > Mathf.Sqrt(agent.AgentConfig.MaxDistance))
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = PlayerTransform.position;
                }
            }

            _timer = agent.AgentConfig.MaxTime;
        }
    }

    public void Exit(AiAgent agent)
    {
 
    }
}
