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
        timer = new Timer(1f);
       Attack(agent);
    }

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
            Debug.Log("DISTANCE");
            agent.StateMachine.ChangeState(StateId.ChasePlayer);
        }
        
   
        
    }

    public void Exit(AiAgent agent)
    {
        foreach(AnimatorControllerParameter parameter in agent.Animator.parameters) {            
            agent.Animator.SetBool(parameter.name, false);            
        }
    }
    
    void Attack(AiAgent agent)
    {
        agent.transform.LookAt(agent.Player);
        randomNumber = Random.Range(1, 4);
        agent.Animator.SetBool("Attack" + randomNumber,true);
        agent.Player.GetComponent<CharacterManager>().TakeDamage(agent.EnemyBase.Damage());
    }
}
