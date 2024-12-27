using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;
    private NavMeshPath _path;

    public void Awake()
    {
        _path = new NavMeshPath();
    }

    public void SetAgent(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void CalculatePathToTarget()
    {
        if (_target == null)
        {
            _agent.ResetPath();
            return;
        }

        if (!_agent.updatePosition)
            _agent.Warp(_agent.transform.position);

        // OPTION 1 - async
        // _agent.SetDestination(_target.position);

        // OPTION 2 - calculated immediately
        Vector3 targetPosition = new(_target.position.x, 0, _target.position.z);
        _agent.CalculatePath(targetPosition, _path);
        if (_path != null) _agent.SetPath(_path);
    }

    public Vector3 GetDesiredDirection()
    {
        return _agent.desiredVelocity.normalized;
    }
}