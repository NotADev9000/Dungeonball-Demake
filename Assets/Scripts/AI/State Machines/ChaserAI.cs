using UnityEngine;

public class ChaserAI : MonoBehaviour
{
    [SerializeField] private Transform _target; // DEBUG

    // [SerializeField] private StatePatrol _patrolState;
    [SerializeField] private StateChase _chaseState;

    private IState _currentState;

    private void Awake()
    {
        _chaseState.InitState();
    }

    private void OnDisable()
    {
        _currentState.Exit();
    }

    private void Start()
    {
        _currentState = _chaseState;
        _chaseState.SetTarget(_target);
        _currentState.Enter();
    }

    private void Update()
    {
        _currentState.Update();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }
}