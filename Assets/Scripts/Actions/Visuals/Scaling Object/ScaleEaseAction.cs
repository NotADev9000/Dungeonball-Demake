using UnityEngine;

public abstract class ScaleEaseAction : IAmActionable
{
    [SerializeField] protected ScaleEaser _scaleEaser;
    [SerializeField] protected float _scaleSpeed = 2.5f;
    public abstract void Execute();
}