using UnityEngine;

namespace ShmupProject
{
    public class SCoreMessageReceiver : MonoBehaviour, IScoreMessageReseiver
    {
        private uint _score;
        private int _scoreMultiplier = 1;

        private float _maxMultiplierDuration = 1.5f;
        private float _multiplierDuration;

        public void AddScore(int score)
        {
            _score += (uint)score * (uint)_scoreMultiplier;

            if (_scoreMultiplier < 10)
                _scoreMultiplier++;

            _multiplierDuration = _maxMultiplierDuration;
            UpdateScore();
        }

        private void Update()
        {
            _multiplierDuration = Mathf.Clamp(_multiplierDuration -= Time.deltaTime, 0, _maxMultiplierDuration);

            if (_multiplierDuration == 0 && _scoreMultiplier > 1)
            {
                _scoreMultiplier = 1;
                UpdateScore();
            }
        }

        private void UpdateScore()
        {
            //Debug.Log(Abbreviate(_score));
            Debug.Log(_score);
        }

        private string Abbreviate(uint value)
        {
            if (_score > 1000000000)
                return $"{_score / 1000000000}B";
            if (_score > 1000000)
                return $"{_score / 1000000}M";
            if (_score > 1000)
                return $"{_score / 1000}K";
            return _score.ToString();
        }
    }
}