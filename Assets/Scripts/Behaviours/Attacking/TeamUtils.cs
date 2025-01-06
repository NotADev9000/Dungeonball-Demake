public enum Teams
{
    Any,
    Player,
    Enemy
}

public interface IHaveATeam
{
    Teams Team { get; set; }
}

public static class TeamUtils
{
    public static bool IsOnDifferentTeam(Teams team1, Teams team2)
    {
        return team1 != team2 || team1 == Teams.Any;
    }
}