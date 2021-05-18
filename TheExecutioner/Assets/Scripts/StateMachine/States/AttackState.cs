using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    private Timer timer;
    private int randomNumber;
    public StateId GetId()
    {
        return StateId.Attack;
    }

    public void Enter(AiAgent agent)
    {
        timer = new Timer(2f);
       Attack(agent);
    }
    /// <summary>
    /// attack the player if the attack timer is over
    /// if the player is further away now, change to chase player state
    /// </summary>
    /// <param name="agent"></param>
    public void Update(AiAgent agent)
    {
        agent.transform.LookAt(agent.Player);
        if(!agent.enabled )
            return;
        
        if (timer.TimerIsOver())
        {
            agent.Animator.SetBool("Attack" + randomNumber,false);
            Attack(agent);
        }
        if (Vector3.Distance(agent.transform.position, agent.Player.transform.position) >= 1.25f)
        {
            agent.StateMachine.ChangeState(StateId.ChasePlayer);
        }
        
    }
    /// <summary>
    /// exit the attack state
    /// </summary>
    /// <param name="agent"></param>
    public void Exit(AiAgent agent)
    {
        foreach(AnimatorControllerParameter parameter in agent.Animator.parameters) {            
            agent.Animator.SetBool(parameter.name, false);            
        }
    }
    /// <summary>
    /// Look at the player
    /// choose a random attack animation
    /// Attack the player
    /// </summary>
    /// <param name="agent"></param>
    void Attack(AiAgent agent)
    {
        agent.transform.LookAt(agent.Player);
        randomNumber = Random.Range(1, 4);
        agent.Animator.SetBool("Attack" + randomNumber,true);
        var target = agent.Player.GetComponent<ITakeDamage>();
        if (target != null)
        {
            target.TakeDamage(agent.EnemyBase.Damage,Vector3.up);
        }
        
        
    }
}
