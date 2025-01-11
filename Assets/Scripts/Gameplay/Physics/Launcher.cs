using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Launcher : MonoBehaviour
{
    [SerializeField] private float _throwForce = 80f;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void Launch(Vector3 direction)
    {
        // detach object from parent so it can be freeeeee (comment this line for weird results...)
        transform.parent = null;
        _collider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction * _throwForce, ForceMode.Impulse);
    }
}
