using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using Cinemachine;
/*
public enum PlayerState
{
    normal,
    dash,
    airdash
}
*/
public class CharacterController : MonoBehaviour
{
    //Assign important components
    [SerializeField]
    protected GameManagement _manager;
    protected ObjectPooler _objectPool;
    protected AudioManager _audioManager;

    protected Rigidbody2D _rb;
    [SerializeField]
    protected BoxCollider2D _hitBox;
    protected Animator _anim;

    protected float _moveSpeed;
    protected float _direction;

    //Assign State
    protected PlayerState _state;

    //Assign Controls
    protected float _horizontal;
    protected float _vertical;
    protected bool _jump;
    protected bool _jumpHold;
    protected bool _dash;
    protected bool _dashHold;
    protected bool _canMove;

    //Assign ground
    protected bool _grounded;
    [SerializeField]
    protected Transform _groundcheck;
    [SerializeField]
    protected float _checkGroundRadius;
    [SerializeField]
    protected LayerMask _whatIsGround;
    protected int _gravity;

    //Assign Jump
    protected bool _canJump;
    protected float _jumpSpeed;
    protected float _jumpTimeCounter;
    protected float _jumpTime;
    protected float _jumpSaverTimer;
    protected bool _jumping;
    protected bool _hasjumped;
    //Assign Respawn timer
    protected float _respawnTimer;
    //Assign Dash
    protected float _dashSpeed;
    protected float _dashTimer;
    protected float _startDashTimer;
    protected bool _dashing;
    protected int _dashDirection;
    protected bool _canDash;
    //Assign Getting hit
    protected bool _getHit;
    protected bool _isAlive;

    //Assign Special effects
    protected bool _landed;
    //Assign Camera
    [SerializeField]
    protected GameObject _camera;
    public CameraShake _cameraShake;
    private float _shakeTime;
    private float _duration;

    [SerializeField]
    private GameObject[] _spawnPoints;
    private int _currentPoint;
    //Cams
    [SerializeField]
    protected CinemachineVirtualCamera _playerCam;

    protected virtual void Start()
    {
        _state = PlayerState.normal;
        _canMove = true;
        _moveSpeed = 8;
        _jumpSpeed = 15;
        _jumpTime = 0.15f;
        _jumpSaverTimer = 0.2f;
        _respawnTimer = 2;
        _startDashTimer = 0.4f;
        _dashTimer = _startDashTimer;
        _dashing = false;
        _dashSpeed = 14;
        _canDash = true;
        _getHit = false;
        _hasjumped = false;
        _objectPool = ObjectPooler.Instance;
        _audioManager = AudioManager.Instance;
        _isAlive = true;
        _gravity = 7;
        _dashDirection = 0;
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _shakeTime = 0.15f;
        _duration = 0.4f;
        _currentPoint = Random.Range(0, _spawnPoints.Length);
        _playerCam.Priority = 5;
    }
    protected virtual void Update()
    {
        #region Animator
        _anim.SetBool("Grounded", _grounded);
        _anim.SetFloat("Move", Mathf.Abs(_horizontal));
        _anim.SetBool("Jumping", _jumping);
        _anim.SetBool("Hurt", _getHit);
        _anim.SetBool("Dashing", _dashing);
        #endregion
        #region flip player
        /*
        if (_horizontal > 0 && _state != PlayerState.airdash)
        {
            _direction = 1;
        }
        else if (_horizontal < 0 && _state != PlayerState.airdash)
        {
            _direction = -1;
        }

        if (_direction == 1)
        {
            _rb.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (_direction == -1)
        {
            _rb.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        #endregion
        #region Dashdirection
        if (_vertical < -0.25f && _state != PlayerState.airdash)
        {
            _dashDirection = -1;
        }
        else if (_vertical > 0.25f && _state != PlayerState.airdash)
        {
            _dashDirection = 1;
        }
        else if(_vertical > -0.25f && _vertical < 0.25f && _state != PlayerState.airdash)
        {
            _dashDirection = 0;
        }
        */
        #endregion
        //Check for ground
        _grounded = Physics2D.OverlapCircle(_groundcheck.position, _checkGroundRadius, _whatIsGround);
        #region Jumping
        if (_grounded)
        {
            _hitBox.size = new Vector2(0.85f, 0.1f);
        }
        else
        {
            _hitBox.size = new Vector2(0.5f, 0.1f);
        }
        if (_grounded && _isAlive)
        {
            _canJump = true;
            _state = PlayerState.normal;
        }
        else if (!_grounded && !_jumping)
        {
            StartCoroutine(JumpSaver());
        }
        else
        {
            _canJump = false;
            _landed = false;
        }
        if (_jump && _canJump && _state == PlayerState.normal)
        {
            _audioManager.Play("Jump");
            _jumping = true;
            _jumpTimeCounter = _jumpTime;
        }

        //Variable jump
        if (_jumping)
        {
            if (_jumpTimeCounter > 0 && _jumpHold)
            {
                _rb.velocity = Vector2.up * _jumpSpeed;
                _jumpTimeCounter -= Time.deltaTime;
            }
            else if (_jumpTimeCounter < 0 || !_jumpHold)
            {
                _jumping = false;
                _hasjumped = false;
            }
        }
        #endregion
        /*
        #region Dashing
        //Initiate dash
        if (_dash && _canDash)
        {
            _dashing = true;
            _rb.velocity = Vector2.zero;
            _canDash = false;
        }
        //Allow dash
        if (!_dashing && _grounded && !_dashHold && !_canDash)
        {
            _canDash = true;
        }
        //If dashing
        if (_dashing && _grounded)
        {
            _state = PlayerState.dash;
        }
        else if (_dashing && !_grounded && _state != PlayerState.dash)
        {
            _state = PlayerState.airdash;
        }
        else
        {
            _state = PlayerState.normal;
            _dashTimer = _startDashTimer;
            _rb.gravityScale = _gravity;
        }
        //Ground dash & Air dash
        if (_state == PlayerState.dash)
        {
            if (_dashTimer > 0 && _dashHold)
            {
                _rb.velocity = new Vector2(_direction * _dashSpeed, _rb.velocity.y);
                _dashTimer -= Time.deltaTime;
            }
            else
            {
                _dashing = false;
                _dashTimer = _startDashTimer;
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = _gravity;
            } 
        }
        else if (_state == PlayerState.airdash)
        {
            if (_dashTimer > 0 && _dashHold && !_grounded)
            {
                _rb.gravityScale = 0;
                _rb.velocity = new Vector2(_direction * _dashSpeed, _dashDirection * _dashSpeed);
                _dashTimer -= Time.deltaTime;
            }
            else
            {
                _dashing = false;
                _dashTimer = _startDashTimer;
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = _gravity;
            }
        }
        #endregion
        */
    }
    protected virtual void FixedUpdate()
    {
        //Moveplayer
        if (_canMove && _state == PlayerState.normal)
        {
            _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
        }
        if (_horizontal != 0 && _grounded)
        {
            _audioManager.Play("Move");
        }
    }

    protected virtual void Bounce()
    {
        _jumpTimeCounter = _jumpTime;
        if (_jumpTimeCounter > 0)
        {
            _rb.velocity = Vector2.up * (_jumpSpeed);
            _jumpTimeCounter -= Time.deltaTime;
        }
        else if (_jumpTimeCounter < 0)
        {
            _jumping = false;
            _jumpTimeCounter = _jumpTime;
        }
    }

    protected IEnumerator Die(GameObject target)
    {
        _audioManager.Play("Hit");
        StartCoroutine(_cameraShake.Shake(_camera, _shakeTime, _duration));
        /*
        if (target.name == "P1")
        {
            target.GetComponent<Player1>()._getHit = true;
            target.GetComponent<Player1>()._canMove = false;
            target.GetComponent<Player1>()._canJump = false;
            target.GetComponent<Player1>()._isAlive = false;
        }
        else
        {
            target.GetComponent<Player2>()._getHit = true;
            target.GetComponent<Player2>()._canMove = false;
            target.GetComponent<Player2>()._canJump = false;
            target.GetComponent<Player2>()._isAlive = false;
        }
        yield return new WaitForSeconds(0.35f);
        if (target.name == "P1")
        {
            target.GetComponent<Player1>()._getHit = false;
            target.GetComponent<Player1>()._canMove = true;
            target.GetComponent<Player1>()._isAlive = true;
        }
        else
        {
            target.GetComponent<Player2>()._getHit = false;
            target.GetComponent<Player2>()._canMove = true;
            target.GetComponent<Player2>()._isAlive = true;
        }
        */
        target.gameObject.SetActive(false);
        yield return new WaitForSeconds(_respawnTimer);
        target.gameObject.SetActive(true);
        target.transform.position = new Vector2(0, 3);
    }

    protected IEnumerator Pitfall(GameObject target)
    {
        StartCoroutine(_cameraShake.Shake(_camera, _shakeTime, _duration));
        target.GetComponent<Collider2D>().enabled = !target.GetComponent<Collider2D>().enabled;
        yield return new WaitForSeconds(_respawnTimer);
        _rb.velocity = Vector2.zero;
        target.GetComponent<Collider2D>().enabled = !target.GetComponent<Collider2D>().enabled;
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _currentPoint = Random.Range(0, _spawnPoints.Length);
        }
        target.transform.position = _spawnPoints[_currentPoint].gameObject.transform.position;
    }

    protected IEnumerator JumpSaver()
    {
        yield return new WaitForSeconds(_jumpSaverTimer);
        if (_grounded)
        {
            _canJump = true;
        }
        else
        {
            _canJump = false;
        }
    }
}
