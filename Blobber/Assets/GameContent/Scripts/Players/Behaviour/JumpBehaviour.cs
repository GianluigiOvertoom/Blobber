using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : PlayerBehaviour
{
    public bool _jumping;

    private bool _canJump;
    private float _jumpSpeed;
    private float _jumpTimeCounter;
    private float _jumpTime;
    private float _jumpSaveTimer;

    protected override void Start()
    {
        base.Start();
        _jumpSpeed = 10f;            
        _jumpTime = 2f;
        _jumpSaveTimer = 0.055f;
    }

    public void Jumping(bool jumpButttonDown, bool jumpButton, float yAxis)
    {
        if (_grounded)
        {
            _canJump = true;
        }
        else if (!_grounded && !_jumping)
        {
            StartCoroutine(JumpSaver());
        }
        else
        {
            _canJump = false;
        }

        if (jumpButttonDown && _canJump && _playerState.Equals(PlayerState.normal))
        {
                _jumping = true;
                _jumpTimeCounter = _jumpTime;
        }

        if (_jumping)
        {
            if (_jumpTimeCounter > 0 && jumpButton)
            {
                _rigidBody2D.velocity = Vector2.up * _jumpSpeed;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else if (_jumpTimeCounter < 0 || !jumpButton)
            {
                _jumping = false;
            }
        }
    }

    private IEnumerator JumpSaver()
    {
        yield return new WaitForSeconds(_jumpSaveTimer);
        if (_grounded)
        {
            _canJump = true;
        }
        else
        {
            _canJump = false;
        }
    }

    public virtual void Bounce()
    {
        _jumpTimeCounter = _jumpTime;
        if (_jumpTimeCounter > 0)
        {
            _rigidBody2D.velocity = Vector2.up * (_jumpSpeed);
            _jumpTimeCounter -= Time.deltaTime;
        }
        else if (_jumpTimeCounter < 0)
        {
            _jumping = false;
            _jumpTimeCounter = _jumpTime;
        }
    }
}
