using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public bool _gamePaused;

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _MainMenu;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _timerMenu;
    [SerializeField] private GameObject _player1ReadyText;
    [SerializeField] private GameObject _player1NotReadyText;
    [SerializeField] private GameObject _player2ReadyText;
    [SerializeField] private GameObject _player2NotReadyText;
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject _p1VictoryScreen;
    [SerializeField] private GameObject _p2VictoryScreen;
    [SerializeField] private ScoreBehaviour _p1Score;
    [SerializeField] private ScoreBehaviour _p2Score;
    [SerializeField] private TextMeshProUGUI _TimerText;
    public bool _player1Ready;
    public bool _player2Ready;
    private PlayerManager _playerManager;

    private bool _doneTiming;
    private int _timer = 3;
    private bool _canPause;

    private void Start()
    {
        _MainMenu.SetActive(true);
        _startMenu.SetActive(false);
        _pauseMenuUI.SetActive(false);
        _timerMenu.SetActive(false);
        _inGameUI.SetActive(false);
        _p1VictoryScreen.SetActive(false);
        _p2VictoryScreen.SetActive(false);
        _playerManager = GetComponent<PlayerManager>();
        _canPause = false;
    }

    private void Update()
    {
        if (!_gamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
        if (_startMenu.activeSelf)
        {
            StartMenuBehaviour();
        }
        if (_timerMenu.activeSelf)
        {
            _playerManager._p1CanMove = false;
            _playerManager._p1CanChangeDirection = false;
            _playerManager._p2CanMove = false;
            _playerManager._p2CanChangeDirection = false;
            StartCountDown();
        }
        if (_p1Score._score == 15)
        {
            P1VictoryScreen();
        }
        if (_p2Score._score == 15)
        {
            P2VictoryScreen();
        }
    }

    private void StartMenuBehaviour()
    {
        if (_player1Ready)
        {
            _player1NotReadyText.SetActive(false);
            _player1ReadyText.SetActive(true);
            _playerManager._player1.gameObject.SetActive(true);
            _playerManager._p1CanMove = false;
            _playerManager._p1CanChangeDirection = false;
        }
        else
        {
            _player1NotReadyText.SetActive(true);
            _player1ReadyText.SetActive(false);
        }

        if (_player2Ready)
        {
            _player2NotReadyText.SetActive(false);
            _player2ReadyText.SetActive(true);
            _playerManager._player2.gameObject.SetActive(true);
            _playerManager._p2CanMove = false;
            _playerManager._p2CanChangeDirection = false;
        }
        else
        {
            _player2NotReadyText.SetActive(true);
            _player2ReadyText.SetActive(false);
        }

        if (XCI.GetButtonDown(XboxButton.Start, XboxController.First))
        {
            _player1Ready = true;
        }
        if (XCI.GetButtonDown(XboxButton.Start, XboxController.Second))
        {
            _player2Ready = true;
        }
        if (_player1Ready && _player2Ready)
        {
            _player1NotReadyText.SetActive(false);
            _player1ReadyText.SetActive(true);
            _playerManager._player1.gameObject.SetActive(true);
            _playerManager._p1CanMove = false;
            _playerManager._p1CanChangeDirection = false;

            _player2NotReadyText.SetActive(false);
            _player2ReadyText.SetActive(true);
            _playerManager._player2.gameObject.SetActive(true);
            _playerManager._p2CanMove = false;
            _playerManager._p2CanChangeDirection = false;

            _doneTiming = false;
            _timerMenu.SetActive(true);
            _startMenu.SetActive(false);
        }
    }

    public void ToStart()
    {
        _startMenu.SetActive(true);
        _MainMenu.SetActive(false);
    }

    private void StartCountDown()
    {
        StartCoroutine(Timer());
        if (_doneTiming)
        {
            _playerManager._p1CanChangeDirection = true;
            _playerManager._p1CanMove = true;
            _playerManager._p2CanChangeDirection = true;
            _playerManager._p2CanMove = true;
            _canPause = true;
            _inGameUI.SetActive(true);
            _timerMenu.SetActive(false);
        }        
    }

    private IEnumerator Timer()
    {
        _TimerText.text = "READY?!";
        yield return new WaitForSeconds(_timer);
        _TimerText.text = " ";
        _doneTiming = true;
    }

    private void P1VictoryScreen()
    {
        _playerManager._p1CanChangeDirection = true;
        _playerManager._p1CanMove = true;
        _playerManager._p2CanChangeDirection = true;
        _playerManager._p2CanMove = true;
        _inGameUI.SetActive(false);
        _p1VictoryScreen.SetActive(true);
        _canPause = false;
    }

    private void P2VictoryScreen()
    {
        _playerManager._p1CanChangeDirection = true;
        _playerManager._p1CanMove = true;
        _playerManager._p2CanChangeDirection = true;
        _playerManager._p2CanMove = true;
        _inGameUI.SetActive(false);
        _p2VictoryScreen.SetActive(true);
        _canPause = false;
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gamePaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }    

    public void Restart()
    {
        _MainMenu.SetActive(false);
        _startMenu.SetActive(false);
        _pauseMenuUI.SetActive(false);
        _timerMenu.SetActive(true);
        _inGameUI.SetActive(false);
        _p1VictoryScreen.SetActive(false);
        _p2VictoryScreen.SetActive(false);
        _player1NotReadyText.SetActive(false);
        _player1ReadyText.SetActive(true);
        _playerManager._player1.gameObject.SetActive(true);
        _playerManager._p1CanMove = false;
        _playerManager._p1CanChangeDirection = false;

        _player2NotReadyText.SetActive(false);
        _player2ReadyText.SetActive(true);
        _playerManager._player2.gameObject.SetActive(true);
        _playerManager._p2CanMove = false;
        _playerManager._p2CanChangeDirection = false;

        _doneTiming = false;
        _timerMenu.SetActive(true);
        _startMenu.SetActive(false);
        _playerManager._player1.GetComponent<DamageBehaviour>()._died = true;
        _playerManager._player2.GetComponent<DamageBehaviour>()._died = true;
        _playerManager._player1.GetComponent<ScoreBehaviour>()._score = 0;
        _playerManager._player2.GetComponent<ScoreBehaviour>()._score = 0;
    }

    private void Pause()
    {
        if (_canPause)
        {
            _pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            _gamePaused = true;
        }
        else
        {
            Resume();
        }
    }
}
