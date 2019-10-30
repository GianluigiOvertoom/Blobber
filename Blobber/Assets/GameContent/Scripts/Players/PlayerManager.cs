using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerManager : MonoBehaviour
{
    public Color _p1Color;
    public Color _p2Color;
    public PlayerBehaviour _player1;
    public PlayerBehaviour _player2;
    public bool _p1CanMove;
    public bool _p2CanMove;
    public bool _p1CanChangeDirection;
    public bool _p2CanChangeDirection;

    [SerializeField] private ParticleSystem _p1SplashParticle;
    [SerializeField] private SpecialEffectBehaviour _specialEffectBehaviourP1;
    [SerializeField] private SpriteRenderer _p1Trail;
    
    [SerializeField] private ParticleSystem _p2SplashParticle;
    [SerializeField] private SpecialEffectBehaviour _specialEffectBehaviourP2;
    [SerializeField] private SpriteRenderer _p2Trail;

    [SerializeField] private GameObject[] _spawnPoints;
    private int _currentPoint;

    private bool _p1Hurt;
    private bool _p2Hurt;
    private float _respawnTimer = 2f;

    private void Start()
    {
        _player1.gameObject.SetActive(false);
        _player2.gameObject.SetActive(false);
    }
    private void Update()
    {
        _p1Trail = _specialEffectBehaviourP1._trailEffect.GetComponent<SpriteRenderer>();
        _player1.GetComponent<SpriteRenderer>().color = _p1Color;
        _p1SplashParticle.startColor = _p1Color;

        _p2Trail = _specialEffectBehaviourP2._trailEffect.GetComponent<SpriteRenderer>();
        _player2.GetComponent<SpriteRenderer>().color = _p2Color;
        _p2SplashParticle.startColor = _p2Color;

        _p1Trail.color = _p1Color;
        _p2Trail.color = _p2Color;

        _player1.GetComponent<MovementBehaviour>()._canMove = _p1CanMove;
        _player2.GetComponent<MovementBehaviour>()._canMove = _p2CanMove;

        _player1.GetComponent<MovementBehaviour>()._canChangeDirection = _p1CanChangeDirection;
        _player2.GetComponent<MovementBehaviour>()._canChangeDirection = _p2CanChangeDirection;

        _p1Hurt = _player1.GetComponent<DamageBehaviour>()._died;
        _p2Hurt = _player2.GetComponent<DamageBehaviour>()._died;
        HurtBehaviour();    
    }
    private void HurtBehaviour()
    {
        if (_p1Hurt)
        {           
            StartCoroutine(Respawn(_player1.gameObject));
            _player1.GetComponent<DamageBehaviour>()._hurt = false;
            _player1.GetComponent<DamageBehaviour>()._died = false;
        }
        if (_p2Hurt)
        {
            StartCoroutine(Respawn(_player2.gameObject));
            _player2.GetComponent<DamageBehaviour>()._hurt = false;
            _player2.GetComponent<DamageBehaviour>()._died = false;
        }
    }

    private IEnumerator Respawn(GameObject target)
    {        
        target.SetActive(false);
        
        yield return new WaitForSeconds(_respawnTimer);
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _currentPoint = Random.Range(0, _spawnPoints.Length);
        }
        target.transform.position = _spawnPoints[_currentPoint].transform.position;
        target.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        target.GetComponent<BoxCollider2D>().enabled = true;
        target.SetActive(true);
        
    }
}
