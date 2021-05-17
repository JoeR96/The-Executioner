using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine
{
    public IState[] states;
    public AiAgent agent;
    public StateId currentState;

    /// <summary>
    /// Constructor to set statemachine to agent
    /// initalize number of states
    /// </summary>
    /// <param name="agent"></param>
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
    /// <summary>
    /// Return a state from an index
    /// </summary>
    /// <param name="stateId"></param>
    /// <returns></returns>
    public IState GetState(StateId stateId)
    {
        int index = (int) stateId;
        return states[index];
    }
    /// <summary>
    /// Changestate using an enum value
    /// </summary>
    /// <param name="newState"></param>
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
    /// <summary>
    /// Register a new state 
    /// </summary>
    /// <param name="state"></param>
    public void RegisterState(IState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }
}
