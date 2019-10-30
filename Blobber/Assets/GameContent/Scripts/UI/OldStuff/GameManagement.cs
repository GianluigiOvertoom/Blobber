
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagement : MonoBehaviour
{
    //Assign Players
    [SerializeField]
    private Player1 _player1;
    [SerializeField]
    private Player2 _player2;

    //Assign Score
    [SerializeField]
    private TextMeshProUGUI _scoreP1;
    [SerializeField]
    private TextMeshProUGUI _scoreP2;

    private float _respawnTimer;

    void Start()
    {
        _respawnTimer = 2;
    }
    void Update()
    {
        //_scoreP1.text = _player1._scoreP1.ToString();
        //_scoreP2.text = _player2._scoreP2.ToString();
        /*
        if (_player1._scoreP1 >= 15)
        {
            Debug.Log("Player 1 has won");
        }
        if (_player2._scoreP2 >= 15)
        {
            Debug.Log("Player 2 has won");
        }
        */
    }
    /*
    public IEnumerator Die(GameObject target)
    {
        //_audioManager.Play("Hit");
        //StartCoroutine(_cameraShake.Shake(_camera, _shakeTime, _duration));
        if (target.name == "P1")
        {
            target.GetComponent<Player1>()._getHit = true;
            target.GetComponent<Player1>()._canMove = false;
            target.GetComponent<Player1>()._canJump = false;
            target.GetComponent<Player1>()._isAlive = false;
        }
        else
        {
            target.GetComponent<Player2>()._getHit = true;
            target.GetComponent<Player2>()._canMove = false;
            target.GetComponent<Player2>()._canJump = false;
            target.GetComponent<Player2>()._isAlive = false;
        }
        yield return new WaitForSeconds(0.35f);
        if (target.name == "P1")
        {
            target.GetComponent<Player1>()._getHit = false;
            target.GetComponent<Player1>()._canMove = true;
            target.GetComponent<Player1>()._isAlive = true;
        }
        else
        {
            target.GetComponent<Player2>()._getHit = false;
            target.GetComponent<Player2>()._canMove = true;
            target.GetComponent<Player2>()._isAlive = true;
        }
        target.gameObject.SetActive(false);
        yield return new WaitForSeconds(_respawnTimer);
        target.gameObject.SetActive(true);
        target.transform.position = new Vector2(0, 3);
    }

    protected IEnumerator Pitfall(GameObject target)
    {
        //StartCoroutine(_cameraShake.Shake(_camera, _shakeTime, _duration));
        target.GetComponent<Collider2D>().enabled = !target.GetComponent<Collider2D>().enabled;
        yield return new WaitForSeconds(_respawnTimer);
        _rb.velocity = Vector2.zero;
        target.GetComponent<Collider2D>().enabled = !target.GetComponent<Collider2D>().enabled;
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _currentPoint = Random.Range(0, _spawnPoints.Length);
        }
        target.transform.position = _spawnPoints[_currentPoint].gameObject.transform.position;
    }
    */
}
