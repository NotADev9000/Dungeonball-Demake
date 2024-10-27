using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Grabbable : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void OnPickup()
    {
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}
