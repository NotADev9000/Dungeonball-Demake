using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] private LayerMask _grabbableLayer;
    public LayerMask GrabbableLayer { get => _grabbableLayer; }

    [SerializeField] private float _grabRange = 2f;
    public float GrabRange { get => _grabRange; }
    [SerializeField] private float _grabSphereRadius = 0.5f;
    public float GrabSphereRadius { get => _grabSphereRadius; }

    public void Grab(Vector3 aimOrigin, Vector3 aimDirection)
    {
        Grabbable grabbable = null;
        if (TryGetLineOfSightObject(aimOrigin, aimDirection, ref grabbable))
        {
            Debug.Log("Grabbable in LOS: " + grabbable.gameObject.name);
        }
        else
        {
            // Debug.Log("NO GRABBABLE OBJECT IN LOS");

            if (TryGetSphereOfSightObject(aimOrigin, aimDirection, ref grabbable))
            {
                Debug.Log("Grabbable in SOS: " + grabbable.gameObject.name);
            }
            else
            {
                Debug.Log("NO GRABBABLE OBJECT IN SOS");
            }
        }
    }

    public bool TryGetLineOfSightObject(Vector3 aimOrigin, Vector3 aimDirection, ref Grabbable grabbable)
    {
        if (Physics.Raycast(aimOrigin, aimDirection, out RaycastHit hit, _grabRange, _grabbableLayer))
        {
            grabbable = hit.collider.gameObject.GetComponent<Grabbable>();
        }
        return grabbable != null;
    }

    public bool TryGetSphereOfSightObject(Vector3 aimOrigin, Vector3 aimDirection, ref Grabbable grabbable)
    {
        if (Physics.SphereCast(aimOrigin, _grabSphereRadius, aimDirection, out RaycastHit hit, _grabRange, _grabbableLayer))
        {
            Debug.Log("SphereCast hit: " + hit.collider.gameObject.name);
            grabbable = hit.collider.gameObject.GetComponent<Grabbable>();
        }
        return grabbable != null;
    }
}
