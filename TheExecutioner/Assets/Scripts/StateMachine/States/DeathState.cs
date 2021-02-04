using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IState
{
    public Vector3 Direction;
    public StateId GetId()
    {
        return StateId.DeathState;
    }

    public void Enter(AiAgent agent)
    {
        agent.Ragdoll.ActivateRagDoll();
        Direction.y = 1;
        agent.Ragdoll.ApplyForce(Direction * agent.AgentConfig.DieForce);
        agent.Mesh.updateWhenOffscreen = true;
    }

    public void Update(AiAgent agent)
    {
        
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
