using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class DeathState : IState
{
    public Vector3 Direction;
    private Timer timer;
    public StateId GetId()
    {
        return StateId.DeathState;
    }

    public void Enter(AiAgent agent)
    {
        timer = new Timer(5f);
        if (agent.navMeshAgent != null)
        {
            agent.navMeshAgent.enabled = false;
        }

        agent.EnemyBase.ActiveSkin.gameObject.SetActive(false);
        agent.EnemyBase.LimbManager.PlayDeathParticles();
        agent.Ragdoll.ActivateRagDoll();
        Direction.y = 1;
        agent.Ragdoll.ApplyForce(Direction * agent.AgentConfig.DieForce);
        agent.Mesh.updateWhenOffscreen = true;
        agent.StartCoroutine(agent.EnemyBase.Die(0f));

    }

    public void Update(AiAgent agent)
    {
     
         
    }

    public void Exit(AiAgent agent)
    {
        
    }
}
