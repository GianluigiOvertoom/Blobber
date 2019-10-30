using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    normal,
    dashing,
    airdashing,
    hit
}

public enum Direction
{
    positive,
    negative
}

public class PlayerBehaviour : MonoBehaviour
{
    protected PlayerState _playerState;
    protected ObjectPooler _objectPool;
    protected Input _input;
    protected Rigidbody2D _rigidBody2D;
    protected MovementBehaviour _movementBehaviour;
    protected JumpBehaviour _jumpBehaviour;
    protected PlayerCheckBehaviour _playerCheckBehaviour;
    protected DashBehaviour _dashBehaviour;
    protected DamageBehaviour _damageBehaviour;
    protected AudioBehaviour _audioBehaviour;
    protected SpecialEffectBehaviour _specialEffectBehaviour;
    protected ScoreBehaviour _scoreBehaviour;
    protected bool _grounded;
    protected float _gravity;
    protected Direction _playerDirection;

    private SpriteRenderer _spriteRenderer;
    private float _xAxis;
    private float _yAxis;
    private bool _jumpButtonDown;
    private bool _jumpButton;
    private bool _dashButtonDown;
    private bool _dashButton;
    
    protected virtual void Start()
    {
        _objectPool = ObjectPooler.Instance;
        
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _input = GetComponent<Input>();
        _movementBehaviour = GetComponent<MovementBehaviour>();
        _jumpBehaviour = GetComponent<JumpBehaviour>();
        _playerCheckBehaviour = GetComponent<PlayerCheckBehaviour>();
        _dashBehaviour = GetComponent<DashBehaviour>();
        _damageBehaviour = GetComponent<DamageBehaviour>();
        _audioBehaviour = GetComponent<AudioBehaviour>();
        _specialEffectBehaviour = GetComponent<SpecialEffectBehaviour>();
        _scoreBehaviour = GetComponent<ScoreBehaviour>();
    }

    protected virtual void Update()
    {
        _yAxis = _input.GetInput()._veticalAxis;
        _jumpButtonDown = _input.GetInput()._jumpButtonDown;
        _jumpButton = _input.GetInput()._jumpButton;
        _dashButtonDown = _input.GetInput()._dashButtonDown;            
        _dashButton = _input.GetInput()._dashButton;

        _grounded = _playerCheckBehaviour._onGround;
        if (!_damageBehaviour._hurt && _playerState.Equals(PlayerState.normal))
        {
            _gravity = 7f;
        }
        else
        {
            _gravity = 0f;
        }

        if (_movementBehaviour._canMove)
        {
            _movementBehaviour.DirectionalBehaviour(_xAxis);
            _jumpBehaviour.Jumping(_jumpButtonDown, _jumpButton, _xAxis);
            _dashBehaviour.Dashing(_dashButtonDown, _dashButton, _yAxis);
        }
                
        if (_damageBehaviour._hurt.Equals(false))
        {
            _playerState = _dashBehaviour._playerState;            
        }
        else
        {
            _playerState = PlayerState.hit;
        }
    }

    protected virtual void FixedUpdate()
    {
        _xAxis = _input.GetInput()._horizontalAxis;
        if (_movementBehaviour._canMove)
        {
            _movementBehaviour.GroundMovement(_xAxis);
        }        
    }

    protected void FlipUnit(Direction direction)
    {
        if (direction.Equals(Direction.positive))
        {
            _spriteRenderer.flipX = false;
        }
        else if (direction.Equals(Direction.negative))
        {
            _spriteRenderer.flipX = true;
        }
    }
    
}
