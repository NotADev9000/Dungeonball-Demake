using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] private LayerMask _grabbableLayer;
    public LayerMask GrabbableLayer { get => _grabbableLayer; }

    [SerializeField] private float _grabRange = 2f;
    public float GrabRange { get => _grabRange; }
    [SerializeField] private float _grabSphereRadius = 0.5f;

    private Grabbable _scannedGrabbable;

    public float GrabSphereRadius { get => _grabSphereRadius; }



    // TODO: Clean this up when Debug statements are no longer needed
    public bool TryGrabGrabbable(Vector3 aimOrigin, Vector3 aimDirection, out Grabbable grabbable)
    {
        grabbable = null;

        if (TryGetLineOfSightObject(aimOrigin, aimDirection, ref grabbable))
        {
            return true;
        }
        else
        {
            // if (TryGetSphereOfSightObject(aimOrigin, aimDirection, ref grabbable))
            if (_scannedGrabbable != null)
            {
                grabbable = _scannedGrabbable;

                // if (_scannedGrabbable?.gameObject.name != grabbable.gameObject.name)
                // {
                //     Debug.Log("SCANNED GRABBABLE: " + _scannedGrabbable?.gameObject.name);
                //     Debug.Log("CLICKED GRABBABLE: " + grabbable?.gameObject.name);
                // }
                // else
                // {
                //     Debug.Log("true");
                // }

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

    public void ScanForGrabbables(Vector3 aimOrigin, Vector3 aimDirection)
    {
        _scannedGrabbable = null;
        if (Physics.SphereCast(aimOrigin, _grabSphereRadius, aimDirection, out RaycastHit hit, _grabRange, _grabbableLayer))
        {
            _scannedGrabbable = hit.collider.gameObject.GetComponent<Grabbable>();
        }
    }

    // private bool TryGetSphereOfSightObject(Vector3 aimOrigin, Vector3 aimDirection, ref Grabbable grabbable)
    // {
    //     if (Physics.SphereCast(aimOrigin, _grabSphereRadius, aimDirection, out RaycastHit hit, _grabRange, _grabbableLayer))
    //     {
    //         grabbable = hit.collider.gameObject.GetComponent<Grabbable>();
    //     }
    //     return grabbable != null;
    // }
}
