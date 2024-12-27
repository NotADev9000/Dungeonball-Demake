using UnityEngine;

public abstract class CollideReactor : MonoBehaviour, ICollide
{
    [field: SerializeField]
    public Team Team { get; set; }

    public abstract void HitSomething();

    public abstract void GetHit(Team hitterTeam);

    protected bool IsOnDifferentTeam(Team otherTeam)
    {
        return Team != otherTeam || Team == Team.Any;
    }
}
