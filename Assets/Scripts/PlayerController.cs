using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader_Player _inputReader;

    [Header("Component References")]
    private CharacterController _controller;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 8f;

    private Vector3 _inputDirection;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 _moveDirection = (transform.right * _inputDirection.x) + (transform.forward * _inputDirection.z);
        Vector3 _velocity = _moveDirection * _moveSpeed;
        _controller.SimpleMove(_velocity);
        // _controller.SimpleMove(transform.forward * _moveSpeed);
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
    }

    private void OnMove(Vector2 directionVector)
    {
        _inputDirection = new Vector3(directionVector.x, 0, directionVector.y).normalized;
    }
}
