using UnityEngine;

public class NullAttackValidator : IValidateAttack
{
    public bool IsAttackValid(GameObject target)
    {
        return true;
    }
}