using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private float _aimRaycastRange = 8f;
    public float AimRaycastRange { get => _aimRaycastRange; }

    /// <summary>
    /// Calculates the direction to throw the object based on the thrower's aim direction
    /// and the point the object is being thrown from.
    /// </summary>
    /// <param name="throwPoint">the point object is being thrown from</param>
    /// <param name="aimOrigin">the origin point thrower is aiming from</param>
    /// <param name="aimDirection">direction thrower is aiming</param>
    /// <returns>direction to throw object</returns>
    private Vector3 GetThrowDirection(Transform throwPoint, Vector3 aimOrigin, Vector3 aimDirection)
    {
        Ray aimRay = new Ray(aimOrigin, aimDirection);
        // store point hit by aiming ray, store max aim distance if nothing hit
        Vector3 aimVector = Physics.Raycast(aimRay, out RaycastHit hit, _aimRaycastRange, 1, QueryTriggerInteraction.Ignore)
            ? hit.point
            : aimRay.GetPoint(_aimRaycastRange);
        // subtract throwing point from aim vector to get throwing direction of throwable
        aimVector -= throwPoint.position;
        return aimVector.normalized;
    }

    public void Throw(IAmThrowable throwable, Transform throwPoint, Vector3 aimOrigin, Vector3 aimDirection)
    {
        Vector3 throwDirection = GetThrowDirection(throwPoint, aimOrigin, aimDirection);
        throwable.Throw(throwDirection);
    }
}
