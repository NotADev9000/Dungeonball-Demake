using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Launcher : MonoBehaviour
{
    [SerializeField] private float _throwForce = 80f;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private Collider _initiatingCollider;

    public event Action OnLaunched;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    // ignores collision with the collider that initiated the launch until the object collides with something else
    // e.g. player throws ball. Ball ignores player's collider until it hits the wall.
    private void OnCollisionExit(Collision other)
    {
        if (_initiatingCollider != null)
        {
            Physics.IgnoreCollision(_collider, _initiatingCollider, false);
            _initiatingCollider = null;
        }
    }

    public void Launch(Vector3 direction, Collider initiatingCollider = null)
    {
        if (initiatingCollider != null)
            IgnoreInitiatingCollider(initiatingCollider);
        // StartCoroutine(IgnoreInitiatingCollider(initiatingCollider));

        // detach object from parent so it can be freeeeee (comment this line for weird results...)
        transform.parent = null;
        _collider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * _throwForce, ForceMode.Impulse);

        OnLaunched?.Invoke();
    }

    private void IgnoreInitiatingCollider(Collider initiatingCollider)
    {
        Physics.IgnoreCollision(_collider, initiatingCollider, true);
        _initiatingCollider = initiatingCollider;
    }

    // private IEnumerator IgnoreInitiatingCollider(Collider initiatingCollider)
    // {
    //     Physics.IgnoreCollision(_collider, initiatingCollider, true);
    //     yield return new WaitForSeconds(0.1f);
    //     Physics.IgnoreCollision(_collider, initiatingCollider, false);
    // }
}
