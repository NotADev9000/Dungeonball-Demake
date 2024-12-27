using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player_Move : MonoBehaviour
{

    [Header("Component References")]
    private CharacterController _controller;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 8f;

    private Vector3 _moveDirection;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 moveDirection = (transform.right * _moveDirection.x) + (transform.forward * _moveDirection.z);
        velocity += moveDirection * _moveSpeed;
        // Debug.Log("velocity before decrease: " + velocity.magnitude);
        velocity -= velocity * 0.2f; // TEST
        // Debug.Log("velocity after decrease: " + velocity.magnitude);
        _controller.SimpleMove(velocity);
    }

    public void SetMoveDirection(Vector2 directionVector)
    {
        _moveDirection = new Vector3(directionVector.x, 0, directionVector.y).normalized;
    }
}
