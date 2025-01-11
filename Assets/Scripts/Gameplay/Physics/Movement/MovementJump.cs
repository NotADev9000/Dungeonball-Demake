using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(GroundSensor))]
public class MovementJump : MonoBehaviour
{
    [SerializeField] private float _jumpStrength = 20f;

    [Tooltip("Time to wait before jumping after landing")]
    [SerializeField] private float _waitTimeBeforeJumping = 0.3f;

    private Rigidbody _rb;
    private GroundSensor _groundSensor;

    private float _jumpTimer;
    private Vector3 _jumpDirection;

    public event Action OnJump;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundSensor = GetComponent<GroundSensor>();
    }

    private void OnEnable()
    {
        _jumpTimer = 0f;
        _jumpDirection = Vector3.zero;
    }

    private void Update()
    {
        if (_groundSensor.IsGrounded)
            _jumpTimer += Time.deltaTime;
        else
            _jumpTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (_jumpTimer >= _waitTimeBeforeJumping && _groundSensor.IsGrounded)
        {
            OnJump?.Invoke();
            _rb.AddForce((_jumpDirection + Vector3.up) * _jumpStrength, ForceMode.VelocityChange);
            _jumpTimer = 0f;
        }
    }

    public void SetJumpDirection(Vector3 jumpDirection)
    {
        jumpDirection.y = 0f;
        _jumpDirection = jumpDirection;
    }
}
