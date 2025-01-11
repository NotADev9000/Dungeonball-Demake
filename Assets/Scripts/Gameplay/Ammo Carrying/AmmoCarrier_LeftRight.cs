using UnityEngine;

public class AmmoCarrier_LeftRight : MonoBehaviour
{
    [SerializeField] private AmmoCarryPoint _leftHold;
    public AmmoCarryPoint LeftHold { get => _leftHold; }
    [SerializeField] private AmmoCarryPoint _rightHold;
    public AmmoCarryPoint RightHold { get => _rightHold; }

    public bool TryGetAmmo(AmmoCarryPoint carrier, out IAmThrowable throwable)
    {
        // get throwable object at carry point
        throwable = carrier.GetComponentInChildren<IAmThrowable>();
        return throwable != null;
    }

    public void PickupAmmo(AmmoCarryPoint carrier, Grabbable grabbable)
    {
        if (carrier.GetComponentInChildren<Grabbable>() != null)
        {
            Debug.LogError(carrier.name + " already has ammo! Should this method be called?");
            return;
        }

        grabbable.transform.parent = carrier.transform;
        grabbable.OnPickup();
    }
}
