using UnityEngine;

public class ChaserAI : MonoBehaviour
{
    [SerializeField] private Transform _target; // Leaving for now...

    [SerializeField] private StateScanForTarget _scanState;
    [SerializeField] private StateChase _chaseState;

    private IState _currentState;

    private void Awake()
    {
        _scanState.SetTarget(transform, _target);
        _chaseState.InitState();
        _chaseState.SetTarget(_target);
    }

    private void OnEnable()
    {
        _scanState.OnTargetInRange += OnTargetInRange;
    }

    private void OnDisable()
    {
        _scanState.OnTargetInRange += OnTargetInRange;
        _currentState.Exit();
    }

    private void Start()
    {
        _currentState = _scanState;
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

    private void OnTargetInRange()
    {
        _currentState.Exit();
        _currentState = _chaseState;
        _currentState.Enter();
    }

#if UNITY_EDITOR
    #region Debug

    [SerializeField] private bool _drawScanCheck = false;

    private void OnDrawGizmosSelected()
    {
        if (_drawScanCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _scanState.TargetRange);
        }
    }

    #endregion
#endif
}