using UnityEngine;

namespace ShmupProject
{
    public class EnemyBulletMoveCommand : IBulletMoveCommand
    {
        private Transform _bullet;
        private float _speed;
        private float _angularSpeed;
        private float _lifeTime;

        private Vector3 _lastPosition;
        private float _bulletRadius;

        public EnemyBulletMoveCommand (Transform bullet, float speed, float angularSpeed, float lifeTime)
        {
            _bullet = bullet;
            _speed = speed;
            _angularSpeed = angularSpeed;
            _lifeTime = lifeTime;
            _lastPosition = _bullet.position;
            GetBulletSize();
        }

        public void Execute(float deltaTime)
        {
            if (_bullet.gameObject.activeSelf)
            {
                _lastPosition = _bullet.position;

                _lifeTime -= deltaTime;
                if (_lifeTime <= 0)
                {
                    ServiceLocator.GetService<ObjectPoolManager>().EnemyBulletsPool.Push(_bullet.gameObject);
                    ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
                }

                //_bullet.transform.Rotate(Vector3.up, _angularSpeed * _speed * deltaTime);
                _bullet.transform.Rotate(Vector3.up, _angularSpeed *180 /Mathf.PI * deltaTime);
                _bullet.Translate(Vector3.forward * _speed * deltaTime, Space.Self);

                if (ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                    _lastPosition, _bulletRadius, _bullet.position - _lastPosition, LayerMask.GetMask(Constants.PlayerLayer)))
                {
                    ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
                    ServiceLocator.GetService<ObjectPoolManager>().EnemyBulletsPool.Push(_bullet.gameObject);
                }
            }
            else
                ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
        }

        private void GetBulletSize()
        {
            var collider =_bullet.gameObject.AddComponent<BoxCollider>();
            _bulletRadius = collider.bounds.extents.x;
            Object.Destroy(collider);
        }
    }
}