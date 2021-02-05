using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public StateMachine StateMachine;
    public StateId InitialState;
    public NavMeshAgent navMeshAgent;
    public Transform Player;
    public AiAgentConfig AgentConfig;

    public Ragdoll Ragdoll;
    public SkinnedMeshRenderer Mesh;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        Ragdoll = GetComponent<Ragdoll>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        StateMachine = new StateMachine(this);
        StateMachine.RegisterState(new ChaseState());
        StateMachine.RegisterState(new DeathState());
        StateMachine.RegisterState(new IdleState());
        StateMachine.ChangeState(InitialState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
    }
    
    void OnAnimatorMove()
    {
    }
}
