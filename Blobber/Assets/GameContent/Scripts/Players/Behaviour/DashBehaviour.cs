using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : PlayerBehaviour
{
    public bool _dashing;

    private float _dashTimer;
    private float _startDashTimer;
    private float _dashXDirection;
    private float _dashYDirection;
    private bool _canDash;
    private float _airDashSpeed = 14f;

    protected override void Start()
    {
        base.Start();
        _dashing = false;
        _startDashTimer = 2.4f;
        _dashTimer = _startDashTimer;
    }

    public void Dashing(bool dashButtonDown, bool dashButton, float yAxis)
    {
        _dashXDirection = _movementBehaviour._directionalSpeed;
        _dashYDirection = yAxis;
        
        if (dashButtonDown && _canDash)
        {
            _dashing = true;
            _rigidBody2D.velocity = Vector2.zero;
            _canDash = false;
        }

        if (!_dashing && _grounded && !dashButton && !_canDash)
        {
            _canDash = true;
        }

        if (_dashing && _grounded)
        {
            _playerState = PlayerState.dashing;
        }
        else if (_dashing && !_grounded && _playerState != PlayerState.dashing)
        {
            _playerState = PlayerState.airdashing;
        }
        else
        {
            _playerState = PlayerState.normal;
            _dashTimer = _startDashTimer;
            _rigidBody2D.gravityScale = _gravity;
        }

        if (_playerState == PlayerState.dashing)
        {
            if (_dashTimer > 0 && dashButton)
            {
                _rigidBody2D.velocity = new Vector2(_dashXDirection * _airDashSpeed, _rigidBody2D.velocity.y);
                _dashTimer -= Time.deltaTime;
            }
            else
            {
                _dashing = false;
                _dashTimer = _startDashTimer;
                _rigidBody2D.velocity = Vector2.zero;
                _rigidBody2D.gravityScale = _gravity;
            }
        }
        else if (_playerState == PlayerState.airdashing)
        {
            if (_dashTimer > 0 && dashButton && !_grounded)
            {
                _rigidBody2D.gravityScale = 0;
                _rigidBody2D.velocity = new Vector2(_dashXDirection * _airDashSpeed, _dashYDirection * _airDashSpeed);
                _dashTimer -= Time.deltaTime;
            }
            else
            {
                _dashing = false;
                _dashTimer = _startDashTimer;
                _rigidBody2D.velocity = Vector2.zero;
                _rigidBody2D.gravityScale = _gravity;
            }
        }
    }
}
