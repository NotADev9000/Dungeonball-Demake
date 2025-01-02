using UnityEngine;

public enum Team
{
    Any,
    Player,
    Enemy
}

public interface ICollide
{
    void OnCollisionEnter(Collision other);
    void GetHit(Team hitterTeam);
    Team Team { get; set; }
}
