using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] private LayerMask _grabbableLayer;
    public LayerMask GrabbableLayer { get => _grabbableLayer; }

    [SerializeField] private float _grabRange = 2f;
    public float GrabRange { get => _grabRange; }
    [SerializeField] private float _grabSphereRadius = 0.5f;
    public float GrabSphereRadius { get => _grabSphereRadius; }

    // TODO: Clean this up when Debug statements are no longer needed
    public bool TryGrabGrabbable(Vector3 aimOrigin, Vector3 aimDirection, out Grabbable grabbable)
    {
        grabbable = null;

        if (TryGetLineOfSightObject(aimOrigin, aimDirection, ref grabbable))
        {
            Debug.Log("Grabbable in LOS: " + grabbable.gameObject.name);
            return true;
        }
        else
        {
            if (TryGetSphereOfSightObject(aimOrigin, aimDirection, ref grabbable))
            {
                Debug.Log("Grabbable in SOS: " + grabbable.gameObject.name);
                return true;
            }
            else
            {
                Debug.Log("NO GRABBABLE OBJECT IN SIGHT");
            }
        }
        return false;
    }

    private bool TryGetLineOfSightObject(Vector3 aimOrigin, Vector3 aimDirection, ref Grabbable grabbable)
    {
        if (Physics.Raycast(aimOrigin, aimDirection, out RaycastHit hit, _grabRange, _grabbableLayer))
        {
            grabbable = hit.collider.gameObject.GetComponent<Grabbable>();
        }
        return grabbable != null;
    }

    private bool TryGetSphereOfSightObject(Vector3 aimOrigin, Vector3 aimDirection, ref Grabbable grabbable)
    {
        if (Physics.SphereCast(aimOrigin, _grabSphereRadius, aimDirection, out RaycastHit hit, _grabRange, _grabbableLayer))
        {
            grabbable = hit.collider.gameObject.GetComponent<Grabbable>();
        }
        return grabbable != null;
    }
}
