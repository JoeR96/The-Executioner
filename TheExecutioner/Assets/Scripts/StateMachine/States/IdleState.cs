using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
    public StateId GetId()
    {
        return StateId.Idle;
    }

    public void Enter(AiAgent agent)
    {
        
    }

    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.Player.position - agent.transform.position;
        if(playerDirection.magnitude > agent.AgentConfig.maxsightDistance)
        {return;}

        Vector3 agentDirection = agent.transform.forward;
        
        playerDirection.Normalize();
        float result = Vector3.Dot(playerDirection, agentDirection);
        if (result > 0.0f)
        {
            agent.StateMachine.ChangeState(StateId.ChasePlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
