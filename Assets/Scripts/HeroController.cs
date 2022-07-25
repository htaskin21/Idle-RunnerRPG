using States;
using UnityEngine;

public class HeroController : CharacterController
{
    [SerializeField] private HeroMovement _heroMovement;

    public HeroMovement HeroMovement => _heroMovement;

    private void Start()
    {
        var runState = _states.Find(x => x.stateType == StateType.Run);
        TransitionToState(runState);
    }
}