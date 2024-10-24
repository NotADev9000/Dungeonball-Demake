using UnityEngine;

public class AmmoCarrier_LeftRight : MonoBehaviour
{
    [SerializeField] private AmmoCarryPoint _leftHold;
    public AmmoCarryPoint LeftHold { get => _leftHold; }
    [SerializeField] private AmmoCarryPoint _rightHold;
    public AmmoCarryPoint RightHold { get => _rightHold; }

    public bool TryGetAmmo(AmmoCarryPoint carrier, out Throwable throwable)
    {
        // get throwable object at carry point
        throwable = carrier.GetComponentInChildren<Throwable>();
        return throwable != null;
    }
}
