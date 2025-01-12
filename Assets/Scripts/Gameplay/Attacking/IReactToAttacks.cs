/// <summary>
/// Interface for objects that can react to attacks.
/// This includes taking damage, playing sounds, etc.
/// </summary>
public interface IReactToAttacks
{
    void OnAttackReceived(Team attackerTeam, int damage);
}
