using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTurnState : State
{
    private StateMachine stateMachine;

    public override void Initialize(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }


    //public virtual void Enter() { }
    //public virtual void Update() { }
    //public virtual void EvaluateTransition() { }
    //public virtual void Exit() { }
}
