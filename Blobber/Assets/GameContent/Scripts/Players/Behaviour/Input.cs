using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Input : PlayerBehaviour
{
    public ControllerInput _controllerInput;
    public XboxController _player;

    [SerializeField] private MainMenu _gameManager;

    private float _analogDeadZone = 0.19f;
    private float _triggerDeadZone;

    public struct ControllerInput
    {
        public float _horizontalAxis;
        public float _veticalAxis;
        public bool _jumpButton;
        public bool _jumpButtonDown;
        public bool _dashButton;
        public bool _dashButtonDown;

        public bool _pause;
    }

    public ControllerInput GetInput()
    {
        ControllerInput input = new ControllerInput
        {
            _horizontalAxis = XCI.GetAxis(XboxAxis.LeftStickX, _player),
            _veticalAxis = XCI.GetAxis(XboxAxis.LeftStickY, _player),
            _jumpButton = XCI.GetButton(XboxButton.A, _player),
            _jumpButtonDown = XCI.GetButtonDown(XboxButton.A, _player),
            _dashButton = XCI.GetButton(XboxButton.X, _player),
            _dashButtonDown = XCI.GetButtonDown(XboxButton.X, _player),
            _pause = XCI.GetButtonDown(XboxButton.Start, _player)
        };
        return input;
    }

    protected override void Update()
    {
        base.Update();
        if (_input.GetInput()._pause && _gameManager._gamePaused.Equals(false))
        {
            _gameManager._gamePaused = true;
        }
        else if (_input.GetInput()._pause && _gameManager._gamePaused.Equals(true))
        {
            _gameManager._gamePaused = false;
        }
    }
}
