using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _timeBetweenPathRecalc = 0.2f;

    [SerializeField] private bool _DebugManualPathRecalc = false;

    [SerializeField] private float _groundCheckDistance = 3f;

    [SerializeField] private float _jumpStrength = 20f;

    private Rigidbody _rb;
    private NavMeshAgent _agent;
    private Vector3[] _currentPathCorners;
    private int _currentPathIndex;
    private float _jumpTimer;
    private float _recalcTimer;

    private bool _groundedLastFrame = false;

    private NavMeshPath _path;

    private void Awake()
    {
        _path = new NavMeshPath();
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;
    }

    /**
     * Need to play around more with using SetDestination vs CalculatePath + SetPath.
     * SD result may not be available immediately, but CP+SP is available same frame.
     * 
     * Also deciding when to recalculate path, either upon landing or every x seconds.
     * but might not matter if doing slime rotation direction automatically.
     *
     * Also need to add higher gravity to slime and experiment with jump strength.
     */

    private void Update()
    {
        if (_DebugManualPathRecalc && Input.GetKeyDown(KeyCode.E) && _target != null)
        {
            // NavMeshPath path = new NavMeshPath();
            // _agent.SetDestination(_target.position);
            _agent.CalculatePath(_target.position, _path);
            _agent.SetPath(_path);


            // if (_path.status == NavMeshPathStatus.PathComplete)
            // {
            //     Debug.Log(_path.corners);
            //     _currentPathCorners = _path.corners;
            //     _currentPathIndex = 0;
            // }
        }

        // Debug.Log(_agent.nextPosition);

        _recalcTimer += Time.deltaTime;
        if (_recalcTimer >= _timeBetweenPathRecalc)
        {
            _recalcTimer = 0;
            // _agent.SetDestination(_target.position);
            _agent.CalculatePath(_target.position, _path);
            if (_path != null) _agent.SetPath(_path);
        }

        if (IsGrounded())
        {
            _jumpTimer += Time.deltaTime;
            if (!_groundedLastFrame)
            {
                _agent.Warp(transform.position);
                // _agent.CalculatePath(_target.position, _path);
                // if (_path != null) _agent.SetPath(_path);
                // _agent.SetDestination(_target.position);
            }
            _groundedLastFrame = true;

            if (_jumpTimer >= 0.2f)
            {
                // _agent.CalculatePath(_target.position, _path);
                // if (_path != null) _agent.SetPath(_path);
            }
        }
        else
        {
            _groundedLastFrame = false;
        }
    }

    private void FixedUpdate()
    {
        // Vector3 nextDirection = GetNextPathDirection();
        // Vector3 nextDirection = (_agent.desiredVelocity).normalized;

        if (_jumpTimer >= 0.3f && IsGrounded())
        {
            Vector3 nextDirection = (_agent.nextPosition - transform.position).normalized;
            // _agent.updatePosition = false;
            _rb.AddForce((nextDirection + Vector3.up) * _jumpStrength, ForceMode.Impulse);
            _jumpTimer = 0;
        }

        // Rotate the agent
        // Quaternion targetRotation = Quaternion.LookRotation(nextDirection);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _agent.angularSpeed * Time.deltaTime);
    }

    private Vector3 GetNextPathDirection()
    {
        if (_currentPathCorners == null || _currentPathIndex >= _currentPathCorners.Length)
        {
            return Vector3.zero;
        }

        Vector3 targetPosition = _currentPathCorners[_currentPathIndex];
        Debug.Log(targetPosition);

        if (Vector3.Distance(transform.position, targetPosition) <= _agent.stoppingDistance)
        {
            _currentPathIndex++;
        }

        return (targetPosition - transform.position).normalized;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * _groundCheckDistance));

        if (_currentPathCorners != null)
        {
            Gizmos.color = Color.red;
            for (int i = _currentPathIndex; i < _currentPathCorners.Length; i++)
            {
                Gizmos.DrawSphere(_currentPathCorners[i], 0.1f);
            }
        }
    }
}
