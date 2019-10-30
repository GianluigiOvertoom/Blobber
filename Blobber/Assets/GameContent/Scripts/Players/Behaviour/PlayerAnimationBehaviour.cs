using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehaviour : PlayerBehaviour
{
    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        _animator.SetBool("Grounded", _grounded);       
        _animator.SetBool("Moving", _movementBehaviour._isMoving);
        _animator.SetBool("Jumping", _jumpBehaviour._jumping);
        _animator.SetBool("Dashing", _dashBehaviour._dashing);
        _animator.SetBool("Hurt", _damageBehaviour._hurt);
    }
}
