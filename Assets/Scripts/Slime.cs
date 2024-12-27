using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _timeBetweenPathRecalc = 0.2f;

    [SerializeField] private float _groundCheckDistance = 0.05f;

    [SerializeField] private float _jumpStrength = 20f;

    [Tooltip("Time to wait before jumping after landing")]
    [SerializeField] private float _waitTimeBeforeJumping = 0.3f;

    [SerializeField] private float _gravityForce = 80f;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackAngle = 60f;

    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private NavMeshAgent _agent;
    private ICollide _collideReactor;

    private Vector3[] _currentPathCorners;
    private int _currentPathIndex;
    private float _pathRecalculationTimer;
    private float _jumpTimer;
    private bool _isAttacking = false;
    private bool _groundedLastFrame = false;

    private NavMeshPath _path;

    private void Awake()
    {
        _path = new NavMeshPath();
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;
        _agent.updateRotation = false;
        _collideReactor = GetComponent<ICollide>();
    }

    /**
     * Need to play around more with using SetDestination vs CalculatePath + SetPath.
     * SD result may not be available immediately, but CP+SP is available same frame.
     *
     * Also the option of calculating path and then manually jumping the slime towards
     * each corner. Slightly tighter control of Slime but no obstacle avoidance.
     * can use getNextPathDirection for this.
     * 
     * Re-calcing path every x seconds and when first landing is best.
     * but make sure to reset timer when landing and re-calcing path.
     *
     * Also need to experiment with gravity & jump strength.
     */

    private void Update()
    {
        UpdatePathRecalculation();
        HandleRotation();
        HandleBehaviour();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_isAttacking) return;

        ICollide hittable = other.gameObject.GetComponent<ICollide>();
        if (hittable != null && IsTargetWithinAttackAngle(other.transform.position))
        {
            hittable.GetHit(_collideReactor.Team);
        }
    }

    private void HandleRotation()
    {
        if (_target == null) return;

        // Quaternion targetRotation = Quaternion.LookRotation(_agent.desiredVelocity.normalized);
        // targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
        Vector3 nextPos = (_target.position - transform.position).normalized;
        nextPos.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(nextPos);
        targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _agent.angularSpeed * Time.deltaTime);
    }

    private void HandleBehaviour()
    {
        if (IsGrounded())
        {
            _isAttacking = false;
            _jumpTimer += Time.deltaTime;
            if (!_groundedLastFrame)
            {
                OnLandedThisFrame();
            }
        }
        else
        {
            _groundedLastFrame = false;
            ApplyGravity();
        }
    }

    private void FixedUpdate()
    {
        // Vector3 nextDirection = GetNextPathDirection();

        if (_jumpTimer >= _waitTimeBeforeJumping && IsGrounded())
        {
            // Vector3 nextDirection = (_agent.nextPosition - transform.position).normalized;
            Vector3 nextDirection = _agent.desiredVelocity.normalized;
            _rb.AddForce((nextDirection + Vector3.up) * _jumpStrength, ForceMode.Impulse);
            _jumpTimer = 0;
            _isAttacking = true;
        }
    }

    private void ApplyGravity()
    {
        _rb.AddForce(Vector3.down * _gravityForce);
    }

    private void OnLandedThisFrame()
    {
        _agent.Warp(transform.position);
        RecalculatePathToTarget();
        _groundedLastFrame = true;
    }

    private Vector3 GetNextPathDirection()
    {
        if (_currentPathCorners == null || _currentPathIndex >= _currentPathCorners.Length)
        {
            return Vector3.zero;
        }

        Vector3 targetPosition = _currentPathCorners[_currentPathIndex];
        // Debug.Log(targetPosition);

        if (Vector3.Distance(transform.position, targetPosition) <= _agent.stoppingDistance)
        {
            _currentPathIndex++;
        }

        return (targetPosition - transform.position).normalized;
    }

    private void UpdatePathRecalculation()
    {
        _pathRecalculationTimer += Time.deltaTime;
        if (_pathRecalculationTimer >= _timeBetweenPathRecalc)
        {
            RecalculatePathToTarget();
        }
    }

    private void RecalculatePathToTarget()
    {
        _pathRecalculationTimer = 0f;
        CalculatePathToTarget();
    }

    private void CalculatePathToTarget()
    {
        if (_target == null)
        {
            _agent.ResetPath();
            return;
        }

        // OPTION 1 - async
        // _agent.SetDestination(_target.position);

        // OPTION 2 - calculated immediately
        Vector3 targetPosition = new(_target.position.x, 0, _target.position.z);
        _agent.CalculatePath(targetPosition, _path);
        if (_path != null) _agent.SetPath(_path);

        // OPTION 3 - get path corners and manually move unit (no obstacle avoidance)
        // _agent.CalculatePath(_target.position, _path);
        // if (_path.status == NavMeshPathStatus.PathComplete)
        // {
        //     _currentPathCorners = _path.corners;
        //     _currentPathIndex = 0;
        // }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, (_boxCollider.bounds.size.y / 2) + _groundCheckDistance);
    }

    private bool IsTargetWithinAttackAngle(Vector3 targetPosition)
    {
        Vector3 targetDirection = (targetPosition - _attackPoint.position).normalized;
        targetDirection.y = 0f;

        Vector3 attackForward = _attackPoint.forward;
        attackForward.y = 0f;

        float angle = Vector3.Angle(attackForward, targetDirection);
        return angle <= _attackAngle / 2;
    }

#if UNITY_EDITOR
    #region Debug

    [SerializeField] private bool _drawGroundCheck = false;
    [SerializeField] private bool _drawAttackAngle = false;

    private void OnDrawGizmosSelected()
    {
        if (_drawGroundCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * ((GetComponent<BoxCollider>().bounds.size.y / 2) + _groundCheckDistance)));
        }

        if (_drawAttackAngle)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_attackPoint.position, _attackPoint.position + _attackPoint.forward * 2);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_attackPoint.position, _attackPoint.position + Quaternion.Euler(0, _attackAngle / 2, 0) * _attackPoint.forward * 2);
            Gizmos.DrawLine(_attackPoint.position, _attackPoint.position + Quaternion.Euler(0, -_attackAngle / 2, 0) * _attackPoint.forward * 2);
        }
    }

    #endregion
#endif
}
