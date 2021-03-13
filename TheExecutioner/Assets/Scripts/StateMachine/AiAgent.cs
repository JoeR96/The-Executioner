using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public StateMachine StateMachine;
    public StateId InitialState;
    public NavMeshAgent navMeshAgent;
    public Transform Player;
    public AiAgentConfig AgentConfig;
    public Animator Animator;
    public Ragdoll Ragdoll;
    public SkinnedMeshRenderer Mesh;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        Animator = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player").transform;
        Ragdoll = GetComponent<Ragdoll>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        StateMachine = new StateMachine(this);
        StateMachine.RegisterState(new ChaseState());
        StateMachine.RegisterState(new DeathState());
        StateMachine.RegisterState(new IdleState());
        StateMachine.RegisterState(new AttackState());
        StateMachine.RegisterState(new TargetState());
        StateMachine.ChangeState(InitialState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        OnAnimatorMove();
    }
    void OnAnimatorMove()
    {
        
            navMeshAgent.velocity = Animator.deltaPosition / Time.deltaTime;
            navMeshAgent.speed = Animator.deltaPosition.magnitude / Time.deltaTime;

    }
}
