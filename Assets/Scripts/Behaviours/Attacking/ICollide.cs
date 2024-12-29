public enum Team
{
    Any,
    Player,
    Enemy
}

public interface ICollide
{
    void HitSomething();
    void GetHit(Team hitterTeam);
    Team Team { get; set; }
}
