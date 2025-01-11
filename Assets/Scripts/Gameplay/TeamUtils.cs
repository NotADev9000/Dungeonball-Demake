public enum Team
{
    Any,
    Player,
    Enemy
}

public interface IHaveATeam
{
    Team Team { get; set; }
}

public static class TeamUtils
{
    public static bool IsOnDifferentTeam(Team team1, Team team2)
    {
        return team1 != team2 || team1 == Team.Any;
    }
}