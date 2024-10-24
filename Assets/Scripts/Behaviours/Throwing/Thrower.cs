using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private float _throwForce = 10f;

    [SerializeField] private float _aimRaycastRange = 8f;

    /// <summary>
    /// Calculates the direction to throw the object based on the thrower's aim direction
    /// and the point the object is being thrown from.
    /// </summary>
    /// <param name="throwPoint">point object is being thrown from</param>
    /// <param name="aimDirection">direction thrower is aiming</param>
    /// <returns>direction to throw object</returns>
    private Vector3 GetThrowDirection(Transform throwPoint, Vector3 aimOrigin, Vector3 aimDirection)
    {
        Ray aimRay = new Ray(aimOrigin, aimDirection);
        // store point hit by aiming ray, store max aim distance if nothing hit
        Vector3 throwDirection = Physics.Raycast(aimRay, out RaycastHit hit, _aimRaycastRange, 1, QueryTriggerInteraction.Ignore)
            ? hit.point
            : aimRay.GetPoint(_aimRaycastRange);
        // subtract throwing point from throw direction to get throwing direction of throwable
        throwDirection -= throwPoint.position;
        return throwDirection.normalized;
    }

    public void Throw(Throwable objectToThrow, Vector3 aimOrigin, Vector3 aimDirection)
    {
        Vector3 throwDirection = GetThrowDirection(objectToThrow.transform, aimOrigin, aimDirection);

        // detach object from parent so it can be free
        objectToThrow.transform.parent = null;

        Collider throwableCollider = objectToThrow.GetComponent<Collider>();
        Rigidbody throwableRigidbody = objectToThrow.GetComponent<Rigidbody>();

        throwableCollider.enabled = true;
        throwableRigidbody.useGravity = true;
        throwableRigidbody.AddForce(throwDirection * _throwForce, ForceMode.Impulse);
    }
}