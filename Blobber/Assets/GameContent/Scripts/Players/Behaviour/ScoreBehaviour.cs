using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreBehaviour : PlayerBehaviour
{
    public int _score;

    [SerializeField] private TextMeshProUGUI _scoreText;

    protected override void Start()
    {
        base.Start();
        _score = 0;
    }
    protected override void Update()
    {
        base.Update();
        if (_score <= 0)
        {
            _score = 0;
        }
        _scoreText.text = _score.ToString();
    }

    public void AddScore()
    {
        _score += 1;
    }
    public void RetractScore()
    {
        _score -= 1;
    }
}
