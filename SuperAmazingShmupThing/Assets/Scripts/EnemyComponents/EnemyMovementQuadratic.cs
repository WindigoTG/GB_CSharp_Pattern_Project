using UnityEngine;

namespace ShmupProject
{
    public sealed class EnemyMovementQuadratic : IMovementEnemy
    {
        private float _coefficientA;
        private float _tgtX;
        private float _tgtZ;
        private Transform _enemy;
        private bool _moveRight;
        private float _speed = 5;

        public EnemyMovementQuadratic(Transform enemy, float tgtX, float tgtZ)
        {
            _enemy = enemy;
            CalculateCoefficients(tgtX, tgtZ);
        }

        public void Move(float deltaTime)
        {
            _enemy.Translate(CalculateMovement(deltaTime) * _speed * deltaTime);
        }

        private Vector3 CalculateMovement(float deltaTime)
        {
            // f(x) = a*x^2 + b;

            float xPos;
            if (_moveRight) xPos = _enemy.position.x + deltaTime;
            else xPos = _enemy.position.x - deltaTime;
            float z = _coefficientA * (xPos - _tgtX) * (xPos - _tgtX) + _tgtZ;

            Vector3 vector = _enemy.position - new Vector3(xPos, _enemy.position.y, z);

            return vector.normalized;
        }

        private void CalculateCoefficients(float tgtX, float tgtZ)
        {
            if (_enemy.position.x > 0)
            {
                _tgtX = -Mathf.Abs(tgtX);
                _moveRight = false;
            }
            else if (_enemy.position.x < 0)
            {
                _tgtX = Mathf.Abs(tgtX);
                _moveRight = true;
            }
            else
            {
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    _tgtX = -Mathf.Abs(tgtX);
                    _moveRight = false;
                }
                else
                {
                    _tgtX = Mathf.Abs(tgtX); ;
                    _moveRight = true;
                }
            }

            _tgtZ = tgtZ;

            _coefficientA = (_enemy.position.z - _tgtZ) / ((_enemy.position.x - _tgtX) * (_enemy.position.x - _tgtX));
        }
    }
}