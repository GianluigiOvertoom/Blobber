using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Player1 : PlayerBehaviour
{
    //Assign Score
    public int _scoreP1;
    public XboxController _player;

    protected override void Start()
    {
        base.Start();
        _player = XboxController.First;
        //_input.GetPlayer(_player);
    }
    protected override void Update()
    {
        base.Update();
        

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
