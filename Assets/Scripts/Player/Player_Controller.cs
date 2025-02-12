using System;
using UnityEngine;

[RequireComponent(typeof(Player_Move), typeof(Player_Look), typeof(AmmoCarrier_LeftRight))]
[RequireComponent(typeof(Thrower), typeof(Grabber))]
public class Player_Controller : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_Player _inputReader;

    // Component References
    private Player_Move _movement;
    private Player_Look _look;
    private AmmoCarrier_LeftRight _ammoCarrier;
    private Thrower _thrower;
    private Grabber _grabber;

    private bool _lastScanFoundGrabbable = false;

    // Events
    public event Action<bool> OnScannedForGrabbablesChange;

    private void Awake()
    {
        _movement = GetComponent<Player_Move>();
        _look = GetComponent<Player_Look>();
        _ammoCarrier = GetComponent<AmmoCarrier_LeftRight>();
        _thrower = GetComponent<Thrower>();
        _grabber = GetComponent<Grabber>();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMovementInput;
        _inputReader.LookEvent += OnLookInput;
        _inputReader.LeftFireEvent += OnLeftFireInput;
        _inputReader.RightFireEvent += OnRightFireInput;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMovementInput;
        _inputReader.LookEvent -= OnLookInput;
        _inputReader.LeftFireEvent -= OnLeftFireInput;
        _inputReader.RightFireEvent -= OnRightFireInput;
    }

    private void Update()
    {
        _grabber.ScanForGrabbables(GetAimOrigin(), GetAimDirection());
        CheckScannedGrabbablesHasChanged();
    }

    private void OnMovementInput(Vector2 directionVector)
    {
        _movement.SetMoveDirection(directionVector);
    }

    private void OnLookInput(Vector2 lookVector)
    {
        _look.SetLookDirection(lookVector);
    }

    private void OnLeftFireInput()
    {
        OnFireInput(_ammoCarrier.LeftHold);
    }

    private void OnRightFireInput()
    {
        OnFireInput(_ammoCarrier.RightHold);
    }

    private void OnFireInput(AmmoCarryPoint ammoCarryPoint)
    {
        // Ideas for dealing with ammo that is in the process of being grabbed:
        // 1. Move carry point to grabbable instantly and parent grabbable to carry point
        //    Then lerp arm to carry point and then lerp arm & carry point back to original position
        // 2. TryGetAmmo from carrier OR get grabbable currently being grabbed
        //    - this means reference to grabbable being grabbed is needed before parenting to carry point

        if (_ammoCarrier.TryGetAmmo(ammoCarryPoint, out IAmThrowable throwable))
        {
            _thrower.Throw(throwable, ammoCarryPoint.transform, GetAimOrigin(), GetAimDirection());
        }
        else
        {
            if (_grabber.TryGrabGrabbable(GetAimOrigin(), GetAimDirection(), out Grabbable grabbable))
            {
                _ammoCarrier.PickupAmmo(ammoCarryPoint, grabbable);
            }
        }
    }

    /// <summary>
    /// Checks if player has changed from "hovering over target" to "not hovering over target" or vice versa.
    /// Invokes event if change is detected.
    /// </summary>
    private void CheckScannedGrabbablesHasChanged()
    {
        // Is player hovering over a target this frame?
        bool foundGrabbableThisFrame = _grabber.ScannedGrabbable != null;
        // If the state has changed since the last scan, invoke event
        if (_lastScanFoundGrabbable != foundGrabbableThisFrame) OnScannedForGrabbablesChange?.Invoke(foundGrabbableThisFrame);
        // Update state for next frame
        _lastScanFoundGrabbable = foundGrabbableThisFrame;
    }

    private Vector3 GetAimOrigin()
    {
        return _look.CameraRoot.position;
    }

    private Vector3 GetAimDirection()
    {
        return _look.CameraRoot.forward;
    }

    #region DEBUGGING

#if UNITY_EDITOR

    [Header("Debugging")]
    [SerializeField] private bool _drawThrowerGizmos = false;
    [SerializeField] private bool _drawGrabberGizmos = false;

    private void OnDrawGizmos()
    {

        var look = GetComponent<Player_Look>();
        var ammoCarrier = GetComponent<AmmoCarrier_LeftRight>();
        var thrower = GetComponent<Thrower>();
        var grabber = GetComponent<Grabber>();

        Vector3 aimOrigin = look.CameraRoot.transform.position;
        Vector3 aimDirection = look.CameraRoot.transform.forward;
        float aimRange = thrower.AimRaycastRange;
        float grabRange = grabber.GrabRange;
        float grabSphereRadius = grabber.GrabSphereRadius;
        LayerMask grabbableLayer = grabber.GrabbableLayer;

        if (_drawThrowerGizmos) DrawThrowGizmos(ammoCarrier, aimOrigin, aimDirection, aimRange);
        if (_drawGrabberGizmos) DrawGrabberGizmos(aimOrigin, aimDirection, grabRange, grabSphereRadius, grabbableLayer);
    }

    private void DrawThrowGizmos(AmmoCarrier_LeftRight ammoCarrier, Vector3 aimOrigin, Vector3 aimDirection, float aimRange)
    {
        // ray straight out from camera, stops drawing if hits something
        Ray aimRay = new Ray(aimOrigin, aimDirection);
        Vector3 aimPoint = Physics.Raycast(aimRay, out RaycastHit hit, aimRange, 1, QueryTriggerInteraction.Ignore)
            ? hit.point
            : aimRay.GetPoint(aimRange);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(aimOrigin, aimPoint);

        // left hold
        Vector3 throwDirection = aimPoint - ammoCarrier.LeftHold.transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(ammoCarrier.LeftHold.transform.position, ammoCarrier.LeftHold.transform.position + throwDirection);

        // right hold
        throwDirection = aimPoint - ammoCarrier.RightHold.transform.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(ammoCarrier.RightHold.transform.position, ammoCarrier.RightHold.transform.position + throwDirection);
    }

    private void DrawGrabberGizmos(Vector3 aimOrigin, Vector3 aimDirection, float grabRange, float grabSphereRadius, LayerMask grabbableLayer)
    {
        Ray aimRay = new Ray(aimOrigin, aimDirection);
        Vector3 aimPoint;
        if (Physics.Raycast(aimRay, out RaycastHit hit, grabRange, grabbableLayer, QueryTriggerInteraction.Ignore))
        {
            aimPoint = hit.point;
            Gizmos.color = Color.green;
        }
        else
        {
            aimPoint = aimRay.GetPoint(grabRange);
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(aimOrigin, aimPoint);

        if (Physics.SphereCast(aimRay, grabSphereRadius, out hit, grabRange, grabbableLayer, QueryTriggerInteraction.Ignore))
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Gizmos.color = transparentGreen;
        }
        else
        {
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);
            Gizmos.color = transparentRed;
        }

        Gizmos.DrawSphere(aimOrigin, grabSphereRadius);
        Gizmos.DrawSphere(aimOrigin + aimDirection * (grabRange / 2), grabSphereRadius);
        Gizmos.DrawSphere(aimOrigin + aimDirection * grabRange, grabSphereRadius);
    }

#endif

    #endregion
}
