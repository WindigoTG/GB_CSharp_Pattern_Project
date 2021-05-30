using UnityEngine;

namespace ShmupProject
{
    public class BulletMoveCommand : IBulletMoveCommand
    {
        private Transform _bullet;
        private float _speed;
        private float _angularSpeed;
        private float _lifeTime;
        private BulletManager _bulletManager;

        public BulletMoveCommand (Transform bullet, float speed, float angularSpeed, float lifeTime, BulletManager bulletManager)
        {
            _bullet = bullet;
            _speed = speed;
            _angularSpeed = angularSpeed;
            _lifeTime = lifeTime;
            _bulletManager = bulletManager;
        }

        public void Execute(float deltaTime)
        {
            if (_bullet.gameObject.activeSelf)
            {
                _lifeTime -= deltaTime;
                if (_lifeTime <= 0)
                {
                    ObjectPoolManager.GetInstance().EnemyBulletsPool.Push(_bullet.gameObject);
                    _bulletManager.RemoveCommand(this);
                }

                //_bullet.transform.Rotate(Vector3.up, _angularSpeed * _speed * deltaTime);
                _bullet.transform.Rotate(Vector3.up, _angularSpeed *180 /Mathf.PI * deltaTime);
                _bullet.Translate(Vector3.forward * _speed * deltaTime, Space.Self);
            }
            else
                _bulletManager.RemoveCommand(this);
        }
    }
}