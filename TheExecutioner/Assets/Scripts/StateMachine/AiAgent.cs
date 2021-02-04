using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public StateMachine StateMachine;
    public StateId InitialState;
    public NavMeshAgent navMeshAgent;
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        StateMachine = new StateMachine(this);
        StateMachine.SetState(new ChaseState());
        StateMachine.ChangeState(InitialState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
    }
}
