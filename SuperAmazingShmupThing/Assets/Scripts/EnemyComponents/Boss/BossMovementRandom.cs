using UnityEngine;

namespace ShmupProject
{
    public class BossMovementRandom : IBossMovement
    {
        private Transform _boss;
        private Vector3 _targetPoint;
        private float _speed = 1.0f;

        private bool _isMoving;
        private float _minMovmentDelay = 2.5f;
        private float _maxMovmentDelay = 5.0f;
        private float _movementDelay;

        private Vector3 _defaultPosition = new Vector3(0, 0, 6);
        private Vector3 _defaultSpawnPosition = new Vector3(0, 0, 11);

        public BossMovementRandom(Transform boss)
        {
            _boss = boss;
            _isMoving = false;
            SetMovementDelay();
        }

        public Transform SetBossToMove
        {
            set
            {
                _boss = value;
                _isMoving = false;
                SetMovementDelay();
            }
        }

        public void Move(float widthLeft, float widthRight, float deltaTime)
        {
            if (_boss != null)
            {
                if (_isMoving)
                {
                    Move(deltaTime);
                }
                else
                {
                    if (_movementDelay > 0)
                        _movementDelay -= deltaTime;
                    else
                    {
                        SetMoveTarget(widthLeft, widthRight);
                        _isMoving = true;
                    }
                }
            }
        }

        private void Move(float deltaTime)
        {
            Vector3 direction = _boss.position - _targetPoint;
            if (direction.sqrMagnitude >= 0.1f)
                _boss.Translate(direction.normalized * deltaTime * _speed);
            else
            {
                _isMoving = false;
                SetMovementDelay();
            }
        }

        private void SetMovementDelay()
        {
            _movementDelay = Random.Range(_minMovmentDelay, _maxMovmentDelay);
        }

        private void SetMoveTarget(float widthLeft, float widthRight)
        {
            float posZ = Random.Range(0, Constants.ScreenBoundZ - 2);
            float posX = Random.Range(Mathf.Min(-widthRight + Constants.ScreenBoundX, -Constants.ScreenBoundX),
                                      Mathf.Max(widthLeft - Constants.ScreenBoundX, Constants.ScreenBoundX));
            _targetPoint = new Vector3(posX, 0, posZ);
        }

        public void ResetBoss()
        {
            _boss.position = _defaultSpawnPosition;
            _targetPoint = _defaultPosition;
            _isMoving = true;
        }
    }
}