using System;
using UnityEngine;

public class DisableAIAction : IAmActionable
{
    private MonoBehaviour _stateMachine;

    public DisableAIAction(MonoBehaviour stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Execute()
    {
        _stateMachine.enabled = false;
    }

}