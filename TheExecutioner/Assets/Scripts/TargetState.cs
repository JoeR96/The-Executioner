using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetState : IState
{

    public StateId GetId()
    {
        return StateId.Target;
    }

    public void Enter(AiAgent agent)
    {
        Debug.Log("HI");
        agent.navMeshAgent.enabled = false;
        agent.Ragdoll.DeactivateRagdoll();
        Debug.Log(agent.Animator.enabled);
    }

    public void Update(AiAgent agent)
    {
        
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
