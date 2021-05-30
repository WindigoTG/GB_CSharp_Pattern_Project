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

                //var rotation = *rotationPtr + *angularSpeedPtr++ * DeltaTime;
                //*rotationPtr = rotation;
                //positionPtr->x += speed * Mathf.Cos(rotation) * DeltaTime;
                //positionPtr->y += speed * Mathf.Sin(rotation) * DeltaTime;

                //var rotation = _bullet.rotation.y + _angularSpeed * deltaTime;
                //_bullet.rotation = Quaternion.Euler(_bullet.rotation.x, rotation, _bullet.rotation.z);

                //_bullet.transform.Rotate(Vector3.up, _angularSpeed * _speed * deltaTime);
                _bullet.transform.Rotate(Vector3.up, _angularSpeed *180 /Mathf.PI * deltaTime);
                _bullet.Translate(Vector3.forward * _speed * deltaTime, Space.Self);

                //_bullet.Translate((Vector3.forward + new Vector3(Mathf.Sin(_angularSpeed), 0.0f, Mathf.Cos(_angularSpeed))) * _speed * deltaTime, Space.Self);
                

                //float posX = _bullet.position.x;
                //float posY = _bullet.position.y;

                //posX += _speed * Mathf.Cos(rotation) * deltaTime;
                //posY += _speed * Mathf.Cos(rotation) * deltaTime;

                //_bullet.position = new Vector3(posX, posY, _bullet.position.z);
            }
            else
                _bulletManager.RemoveCommand(this);
        }
    }
}