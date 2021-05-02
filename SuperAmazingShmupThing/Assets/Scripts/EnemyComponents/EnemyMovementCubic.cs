using UnityEngine;

namespace ShmupProject
{
    public sealed class EnemyMovementCubic : IMovementEnemy
    {
        private float _coefficientA;
        private float _coefficientB;
        private float _tgtX;
        private float _tgtZ;
        private Transform _enemy;
        private bool _moveRight;
        private float _arbitraryMagicSpeedModifier = 0.05f;
        private float _minXpos = 2;
        private float _maxXpos = 4;

        public EnemyMovementCubic(Transform enemy, float tgtX, float tgtZ)
        {
            _enemy = enemy;
            CalculateCoefficients(tgtX, tgtZ);
        }

        public void Move(float deltaTime)
        {
            _enemy.Translate(CalculateMovement(deltaTime) * _arbitraryMagicSpeedModifier);
        }

        private Vector3 CalculateMovement(float deltaTime)
        {
            // f(x) = a*x^3 + b*x;

            float xPos;
            if (_moveRight) xPos = _enemy.position.x + deltaTime;
            else xPos = _enemy.position.x - deltaTime;

            float zPos = _coefficientA * xPos * xPos * xPos + _coefficientB * xPos;

            Vector3 vector = _enemy.position - new Vector3(xPos, _enemy.position.y, zPos);

            return vector.normalized;
        }

        private void CalculateCoefficients(float tgtX, float tgtZ)
        {
            

            if (_enemy.position.x > 1)
            {
                if (_enemy.position.x < _minXpos)
                    _enemy.position = new Vector3(_enemy.position.x * 2, _enemy.position.y, _enemy.position.z);
                _moveRight = false;
            }
            else if (_enemy.position.x < -1)
            {
                if (_enemy.position.x > -_minXpos)
                    _enemy.position = new Vector3(_enemy.position.x * 2, _enemy.position.y, _enemy.position.z);
                _moveRight = true;
            }
            else
            {
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    _moveRight = false;
                    _enemy.position = new Vector3(_maxXpos, _enemy.position.y, _enemy.position.z);
                }
                else
                {
                    _moveRight = true;
                    _enemy.position = new Vector3(-_maxXpos, _enemy.position.y, _enemy.position.z);
                }
            }

            if (Mathf.Abs(tgtX) <= Mathf.Abs(_enemy.position.x) / 2)
                _tgtX = Mathf.Abs(tgtX);
            else
                _tgtX = Mathf.Abs(_enemy.position.x) / 2;
            _tgtZ = Mathf.Abs(tgtZ);

            _coefficientA = _tgtZ / (_enemy.position.x * _enemy.position.x * _enemy.position.x - _tgtX * _tgtX * _enemy.position.x);
            _coefficientB = -3 * _coefficientA * _tgtX * _tgtX;
        }
    }
}