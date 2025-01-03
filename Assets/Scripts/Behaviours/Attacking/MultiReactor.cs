using UnityEngine;

public class MultiReactor : CollideReactor
{
    [SerializeField] private VFXFlash _vfxFlash = null;
    [SerializeField] private VFXFlashData_SO _vfxFlashData = null;

    private Damageable _damageable;
    private AttackHandler _attackHandler;
    // private SoundPlay _soundPlay;

    public void Init(Damageable damageable, AttackHandler attackHandler)
    {
        _damageable = damageable;
        _attackHandler = attackHandler;
    }

    public override void OnCollisionEnter(Collision other)
    {
        // Debug.Log(gameObject.name + " collided with something!");
        _attackHandler.OnAttack(other.gameObject, Team);
    }

    public override void GetHit(Team attackerTeam)
    {
        // put logic in hit handler
        // then have different hit handlers for different types of hits
        if (IsOnDifferentTeam(attackerTeam))
        {
            // Debug.Log(gameObject.name + " got hit!");
            _damageable.Damage(10);
            if (_vfxFlash != null && _vfxFlashData != null)
            {
                _vfxFlash.Play(_vfxFlashData);
            }
        }
    }
}
