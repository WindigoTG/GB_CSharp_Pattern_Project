using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

namespace ShmupProject
{
    public class ScoreTracker : IUpdateable
    {
        private uint _score;
        private int _scoreMultiplier = 1;

        private TextMeshProUGUI _scoreText;
        private TextMeshProUGUI _multiplierText;
        private RectTransform _multiplierCounterBar;

        private float _maxMultiplierDuration = 1.5f;
        private float _multiplierDuration;

        private int _counterMaxWidth;
        private Image _multiplierCounter;

        private EnemyController _enemyController;

        public ScoreTracker(EnemyController enemyController)
        {
            _multiplierText = Object.FindObjectOfType<BillboardRenderer>().GetComponent<TextMeshProUGUI>();
            _scoreText = Object.FindObjectOfType<AimConstraint>().GetComponent<TextMeshProUGUI>();
            _multiplierCounterBar = Object.FindObjectOfType<SpriteMask>().GetComponent<RectTransform>();
            _multiplierCounter = _multiplierCounterBar.GetComponent<Image>();
            _enemyController = enemyController;

            _counterMaxWidth = Screen.width - 20;
        }

        public void AddScore(int score)
        {
            _score += (uint)score * (uint)_scoreMultiplier;

            if (_scoreMultiplier < 10)
                _scoreMultiplier++;

            _multiplierDuration = _maxMultiplierDuration;
            UpdateScore();
        }

        public void UpdateRegular(float deltaTime)
        {
            if (_enemyController.IsEnemyWaveInProgress)
                _multiplierDuration = Mathf.Clamp(_multiplierDuration -= deltaTime, 0, _maxMultiplierDuration);

            if (_multiplierDuration == 0 && _scoreMultiplier > 1)
            {
                _scoreMultiplier = 1;
                UpdateScore();
            }

            _multiplierCounterBar.sizeDelta = new Vector2(_multiplierDuration/_maxMultiplierDuration * _counterMaxWidth, 5);
            _multiplierCounter.color = Color.Lerp(Color.red, Color.green, _multiplierDuration / _maxMultiplierDuration);
        }

        private void UpdateScore()
        {
            _scoreText.text = Abbreviate(_score);

            _multiplierText.text = $"x{_scoreMultiplier}";
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