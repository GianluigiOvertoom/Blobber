using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckBehaviour : PlayerBehaviour
{
    public bool _onGround;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    private float _checkGroundRadius = 0.439638f;
    private float _fallTimer = 0.2f;

    protected override void Update()
    {
        base.Update();
        _onGround = Physics2D.OverlapCircle(_groundCheck.position, _checkGroundRadius, _whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _checkGroundRadius);
    }
}
