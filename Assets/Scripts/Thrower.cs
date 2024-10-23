using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Transform[] _ammoHoldPoints;

    [SerializeField] private float _throwForce = 10f;

    [SerializeField] private float _aimRaycastRange = 8f;

    private Vector3 GetThrowDirection(Ray aimDirection, Transform holdPoint)
    {
        // store point hit by aiming ray, store max aim distance if nothing hit
        Vector3 throwDirection = Physics.Raycast(aimDirection, out RaycastHit hit, _aimRaycastRange, 1, QueryTriggerInteraction.Ignore)
            ? hit.point
            : aimDirection.GetPoint(_aimRaycastRange);
        // subtract hold point position from throw direction to get throwing direction of throwable
        throwDirection -= holdPoint.position;
        return throwDirection.normalized;
    }

    private void Throw(Ray aimDirection, Transform holdPoint)
    {
        Vector3 throwDirection = GetThrowDirection(aimDirection, holdPoint);

        GameObject ammo = holdPoint.GetChild(0).gameObject;
        Collider ammoCollider = ammo.GetComponent<Collider>();
        Rigidbody ammoRigidbody = ammo.GetComponent<Rigidbody>();

        ammo.transform.parent = null;
        ammoCollider.enabled = true;
        ammoRigidbody.useGravity = true;
        ammoRigidbody.AddForce(throwDirection * _throwForce, ForceMode.Impulse);
    }
}