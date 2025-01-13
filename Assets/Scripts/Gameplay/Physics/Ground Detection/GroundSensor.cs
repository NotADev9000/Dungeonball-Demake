using System;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistance = 0.05f;

    private BoxCollider _boxCollider;

    public bool IsGrounded { get; private set; }
    private bool _wasGroundedLastFrame;
    public bool LandedThisFrame { get; private set; }

    public event Action OnLandedThisFrame;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (CheckGrounded())
        {
            IsGrounded = true;
            LandedThisFrame = !_wasGroundedLastFrame;
            if (LandedThisFrame) OnLandedThisFrame?.Invoke();
            _wasGroundedLastFrame = true;
        }
        else
        {
            IsGrounded = false;
            LandedThisFrame = false;
            _wasGroundedLastFrame = false;
        }
    }

    private bool CheckGrounded()
    {
        Vector3 halfExtents = _boxCollider.bounds.size / 2;
        Vector3 boxCenter = new(transform.position.x, transform.position.y + halfExtents.y, transform.position.z);
        return Physics.BoxCast(boxCenter, halfExtents, Vector3.down, Quaternion.identity, _groundCheckDistance);
        // return Physics.Raycast(transform.position, Vector3.down, (_boxCollider.bounds.size.y / 2) + _groundCheckDistance);
    }

#if UNITY_EDITOR
    #region Debug

    [SerializeField] private bool _drawGroundCheck = false;

    private void OnDrawGizmosSelected()
    {
        if (_drawGroundCheck)
        {
            Collider col = GetComponent<BoxCollider>();
            Gizmos.color = Color.red;
            Vector3 from = col.bounds.center + (Vector3.down * ((col.bounds.size.y / 2) + (_groundCheckDistance / 2)));
            // Vector3 to = from + (Vector3.down * _groundCheckDistance);
            Vector3 to = new(col.bounds.size.x, _groundCheckDistance, col.bounds.size.z);
            Gizmos.DrawCube(from, to);
        }
    }

    #endregion
#endif
}
