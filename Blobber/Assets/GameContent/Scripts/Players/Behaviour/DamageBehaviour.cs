using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DamageBehaviour : PlayerBehaviour
{
    public bool _hurt;
    public bool _died;

    [SerializeField] private BoxCollider2D _hurtBox;
    
    protected override void Update()
    {
        base.Update();
        if (_hurt)
        {
            _movementBehaviour._canChangeDirection = false;
            _movementBehaviour._canMove = false;
            _hurtBox.enabled = false;
        }
        else
        {
            _hurtBox.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HurtBox"))
        {
            _scoreBehaviour.AddScore();
            _jumpBehaviour.Bounce();
            collision.GetComponentInParent<DamageBehaviour>()._hurt = true;
        }
        else if (collision.CompareTag("Danger"))
        {
            _scoreBehaviour.RetractScore();
            _hurt = true;
        }
    }

    public void DisableBoxCollider()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Die()
    {
        _died = true;
    }
}
