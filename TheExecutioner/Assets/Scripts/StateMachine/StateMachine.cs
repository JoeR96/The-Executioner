using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine
{
    public IState[] states;
    public AiAgent agent;
    public StateId currentState;

    public StateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numOfStates = System.Enum.GetNames(typeof(StateId)).Length;
        states = new IState[numOfStates];
    }
    public void SetState(IState state)
    {
        int index = (int) state.GetId();
        states[index] = state;
    }

    public IState GetState(StateId stateId)
    {
        int index = (int) stateId;
        return states[index];
    }

    public void ChangeState(StateId newState)
    {
        GetState(currentState).Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void RegisterState(IState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }
}
