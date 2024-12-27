using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Throwable : MonoBehaviour
{
    private ICollide _collideReactor;

    private void Awake()
    {
        _collideReactor = GetComponent<ICollide>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _collideReactor.HitSomething();
        if (other.gameObject.TryGetComponent<ICollide>(out var hittable))
            hittable.GetHit(_collideReactor.Team);
    }
}
