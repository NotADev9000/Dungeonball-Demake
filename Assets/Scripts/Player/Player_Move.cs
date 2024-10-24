using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Move : MonoBehaviour
{

    [Header("Component References")]
    private CharacterController _controller;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 8f;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 moveDirection = (transform.right * _moveDirection.x) + (transform.forward * _moveDirection.z);
        Vector3 velocity = moveDirection * _moveSpeed;
        _controller.SimpleMove(velocity);
    }

    public void SetMoveDirection(Vector2 directionVector)
    {
        _moveDirection = new Vector3(directionVector.x, 0, directionVector.y).normalized;
    }
}
