using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : PlayerBehaviour
{
    private AudioManager _audioManager;
    private bool _landed;
    private bool _hasJumped;
    private bool _hasBeenHurt;

    protected override void Start()
    {
        base.Start();
        _audioManager = AudioManager.Instance;
    }

    protected override void Update()
    {
        base.Update();
        if (_grounded && !_landed)
        {
            _audioManager.Play("Land");
            _landed = true;
        }
        else if (!_grounded)
        {
            _specialEffectBehaviour.SpawnSplashPartical();
            _landed = false;
        }
        if (_jumpBehaviour._jumping && !_hasJumped)
        {
            _audioManager.Play("Jump");
            _hasJumped = true;
        }
        else if(!_jumpBehaviour._jumping)
        {
            _hasJumped = false;
        }

        if (_damageBehaviour._hurt && !_hasBeenHurt)
        {
            _audioManager.Play("Hit");
            _hasBeenHurt = true;
        }
        else if (!_damageBehaviour._hurt)
        {
            _hasBeenHurt = false;
        }
    }

    public void PlayMoveSound()
    {
        _audioManager.Play("Move");
    }
}
