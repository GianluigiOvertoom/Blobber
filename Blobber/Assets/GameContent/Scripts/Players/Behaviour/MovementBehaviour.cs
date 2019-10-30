using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : PlayerBehaviour
{
    public float _directionalSpeed;
    public bool _isMoving;
    public bool _canChangeDirection;
    public bool _canMove;

    private float _movementSpeed = 8f;
    private float _dashSpeed = 14f;

    protected override void Start()
    {
        base.Start();
        _directionalSpeed = 1;
    }

    public void DirectionalBehaviour(float xAxis)
    {
        if (xAxis > 0 && _canChangeDirection)
        {
            if (_canChangeDirection)
            {
                _playerDirection = Direction.positive;
            }
            _directionalSpeed = 1;
        }
        else if (xAxis < 0 && _canChangeDirection)
        {
            if (_canChangeDirection)
            {
                _playerDirection = Direction.negative;
            }
            _directionalSpeed = -1;
        }

        FlipUnit(_playerDirection);

        if (xAxis != 0 && _grounded)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    public void GroundMovement(float xAxis)
    {
        if (_playerState.Equals(PlayerState.normal))
        {
            _rigidBody2D.velocity = new Vector2(xAxis * _movementSpeed, _rigidBody2D.velocity.y);
        }
    }
}
