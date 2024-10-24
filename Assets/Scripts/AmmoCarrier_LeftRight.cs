using UnityEngine;

public class AmmoCarrier_LeftRight : MonoBehaviour
{
    [SerializeField] private AmmoCarrierPoint _leftHold;
    public AmmoCarrierPoint LeftHold { get => _leftHold; }
    [SerializeField] private AmmoCarrierPoint _rightHold;
    public AmmoCarrierPoint RightHold { get => _rightHold; }

    public bool TryGetAmmo(AmmoCarrierPoint carrier, out IAmThrowable throwable)
    {
        // get throwable object at carry point
        throwable = carrier.GetComponentInChildren<IAmThrowable>();
        return throwable != null;
    }
}
