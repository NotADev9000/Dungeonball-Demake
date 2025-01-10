using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(GroundSensor))]
public class ApplyExtraGravity : MonoBehaviour
{
    [SerializeField] private float _gravityForce = 80f;

    private Rigidbody _rb;
    private GroundSensor _groundSensor;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundSensor = GetComponent<GroundSensor>();
    }

    void FixedUpdate()
    {
        if (!_groundSensor.IsGrounded)
            _rb.AddForce(Vector3.down * _gravityForce, ForceMode.Acceleration);
    }
}
