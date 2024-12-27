using UnityEngine;
using UnityEngine.AI;

public class InputController_AI : MonoBehaviour
{
    [SerializeField] private Pathfinder _pathfinder;
    public Pathfinder Pathfinder => _pathfinder;
    [SerializeField] private MovementJump _jumpMovement;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _pathfinder.SetAgent(_agent);
    }

    public void OnEnable()
    {
        _agent.updatePosition = false;
        // _agent.updateRotation = false;
        _jumpMovement.enabled = true;
    }

    public void OnDisable()
    {
        _jumpMovement.enabled = false;
    }

    public void Update()
    {
        Vector3 dir = _pathfinder.GetDesiredDirection();
        _jumpMovement.SetJumpDirection(dir);
    }
}
