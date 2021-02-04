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
        agent.navMeshAgent.enabled = false;
        agent.Ragdoll.DeactivateRagdoll();
        Direction.y = 1;
        agent.Ragdoll.ApplyForce(Direction * agent.AgentConfig.DieForce);
        agent.Mesh.updateWhenOffscreen = true;
        Debug.Log("Entered");
    }

    public void Update(AiAgent agent)
    {
        
    }

    public void Exit(AiAgent agent)
    {
        
    }

    private IEnumerator Die(AiAgent agent)
    {
        yield return new WaitForSeconds(1f);
        Object.Destroy(agent.gameObject);
    }
}
