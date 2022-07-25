using System.Collections.Generic;
using System.Linq;
using States;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] 
    private AnimationController animationController;
    public AnimationController AnimationController => animationController;
    
    public State currentState = null;
    public State previousState = null;

    internal List<State> _states;

    private void Awake()
    {
        _states = new List<State>();
        _states = GetComponentsInChildren<State>().ToList();

        foreach (var state in _states)
        {
            state.InitializeState(this);
        }
    }

    internal void TransitionToState(State nextState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        previousState = currentState;
        currentState = nextState;
        currentState.Enter();
    }
}