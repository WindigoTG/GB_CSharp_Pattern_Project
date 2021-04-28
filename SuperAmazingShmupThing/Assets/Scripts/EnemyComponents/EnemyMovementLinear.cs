using UnityEngine;

namespace ShmupProject
{
    public class EnemyMovementLinear : IMovementEnemy
    {
        private float _coefficientA;
        private float _coefficientB;
        private Transform _enemy;
        private bool _moveRight;
        private float _arbitraryMagicSpeedModifier = 0.05f;

        public EnemyMovementLinear(Transform enemy, float tgtX, float tgtZ)
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
            // f(x) = a*x + b;

            float xPos;
            if (_moveRight) xPos = _enemy.position.x + deltaTime;
            else xPos = _enemy.position.x - deltaTime;
            float zPos = _coefficientA * xPos + _coefficientB;
            Vector3 vector = _enemy.position - new Vector3(xPos, _enemy.position.y, zPos);

            return vector.normalized;
        }

        private void CalculateCoefficients(float tgtX, float tgtZ)
        {
            if (_enemy.position.x > 0)
            {
                _moveRight = false;
            }
            else if (_enemy.position.x < 0)
            {
                _moveRight = true;
            }
            else
            {
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    _moveRight = false;
                }
                else
                {
                    _moveRight = true;
                }
            }

            if (tgtX - _enemy.position.x == 0)
            {
                _coefficientA = 0;
            }
            else
                _coefficientA = (tgtZ - _enemy.position.z) / (tgtX - _enemy.position.x);

            if ((_moveRight && _coefficientA > 0) || (!_moveRight && _coefficientA < 0))
                _coefficientA = -_coefficientA;

            _coefficientB = _enemy.position.z - _coefficientA * _enemy.position.x;
        }
    }
}