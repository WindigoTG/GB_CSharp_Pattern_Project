using UnityEngine;

namespace ShmupProject
{
    public class PlayerBulletMoveCommand : IBulletMoveCommand
    {
        private Transform _bullet;
        private float _speed;
        private float _lifeTime;

        private Vector3 _lastPosition;
        private float _bulletRadius;

        public PlayerBulletMoveCommand(Transform bullet, float speed, float lifeTime)
        {
            _bullet = bullet;
            _speed = speed;
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
                    ServiceLocator.GetService<BulletPoolManager>().PlayerBulletsPool.Push(_bullet.gameObject);
                    ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
                }

                _bullet.Translate(Vector3.forward * _speed * deltaTime, Space.Self);

                if (ServiceLocator.GetService<CollisionManager>().CheckCollisions(
                    _lastPosition, _bulletRadius, _bullet.position - _lastPosition, LayerMask.GetMask(MagicStrings.EnemyLayer)))
                {
                    ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
                    ServiceLocator.GetService<BulletPoolManager>().PlayerBulletsPool.Push(_bullet.gameObject);
                }
            }
            else
                ServiceLocator.GetService<BulletManager>().RemoveCommand(this);
        }

        private void GetBulletSize()
        {
            var collider =_bullet.gameObject.AddComponent<BoxCollider>();
            _bulletRadius = collider.bounds.extents.z;
            Object.Destroy(collider);
        }
    }
}