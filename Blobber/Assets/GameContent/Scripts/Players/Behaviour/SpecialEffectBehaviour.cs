using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class SpecialEffectBehaviour : PlayerBehaviour
{
    public GameObject _trailEffect;

    [SerializeField] private ParticleSystem _splashEffect;
    [SerializeField] private CinemachineVirtualCamera _camera;
   
    private float _timeBetweenSpawns;
    private float _startTimer = 0.025f;
    

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (_input.GetInput()._dashButtonDown)
        {
            _camera.transform.DOComplete();
            _camera.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        }
        if (!_grounded)
        {
            if (_timeBetweenSpawns <= 0f && this.gameObject.tag.Equals("Player1"))
            {
                _trailEffect = _objectPool.SpawnFromPool("GreenAirTrail", transform.position, Quaternion.identity);
                _timeBetweenSpawns = _startTimer;
            }
            else
            {
                _timeBetweenSpawns -= Time.deltaTime;
            }
            if (_timeBetweenSpawns <= 0f && this.gameObject.tag.Equals("Player2"))
            {
                _trailEffect = _objectPool.SpawnFromPool("PurpleAirTrail", transform.position, Quaternion.identity);
                _timeBetweenSpawns = _startTimer;
            }
            else
            {
                _timeBetweenSpawns -= Time.deltaTime;
            }
        }
    }

    public virtual void SpawnSplashPartical()
    {
        _splashEffect.Play();
    }

    //All green's Special effects

    public virtual void SpawnGreenTrail()
    {
        _objectPool.SpawnFromPool("GreenTrail", transform.position, Quaternion.identity);
    }

    public virtual void SpawnGreenSplash()
    {

        _objectPool.SpawnFromPool("GreenSplash", transform.position, Quaternion.identity);
    }

    //All purple's special effects

    public virtual void SpawnPurpleTrail()
    {
        _objectPool.SpawnFromPool("PurpleTrail", transform.position, Quaternion.identity);
    }

    public virtual void SpawnPurpleSplash()
    {
        _objectPool.SpawnFromPool("PurpleSplash", transform.position, Quaternion.identity);
    }
}
