using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffect : MonoBehaviour
{
    private Animator _anim;
    private bool _alive;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _alive = false;
    }
    void Update()
    {
        _anim.SetBool("Alive", _alive);
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > _anim.GetCurrentAnimatorStateInfo(0).length)
        {
            _alive = false;
        }
        if (!_alive)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnBecameVisible()
    {
        _alive = true;
    }
}
