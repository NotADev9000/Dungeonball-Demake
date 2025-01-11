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
        return Physics.Raycast(transform.position, Vector3.down, (_boxCollider.bounds.size.y / 2) + _groundCheckDistance);
    }

#if UNITY_EDITOR
    #region Debug

    [SerializeField] private bool _drawGroundCheck = false;

    private void OnDrawGizmosSelected()
    {
        if (_drawGroundCheck)
        {
            Gizmos.color = Color.red;
            Vector3 from = transform.position + (Vector3.down * (GetComponent<BoxCollider>().bounds.size.y / 2));
            Vector3 to = from + (Vector3.down * _groundCheckDistance);
            Gizmos.DrawLine(from, to);
        }
    }

    #endregion
#endif
}
