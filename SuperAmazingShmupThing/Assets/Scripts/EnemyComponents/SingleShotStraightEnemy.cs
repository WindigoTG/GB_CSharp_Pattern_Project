using UnityEngine;

namespace ShmupProject
{
    public sealed class SingleShotStraightEnemy : EnemyWeapon
    {
        public SingleShotStraightEnemy()
        {
            float bulletSpeed = 5.0f;
            float fireDelay = 2.0f;
            float bulletLifeTime = 5.0f;

            _bullet = new SingleBullet(BulletOwner.Enemy);
            _config = new BullletConfig(bulletSpeed, 0, bulletLifeTime, fireDelay, 1, 1, 1, 0, 0);
         }

        public override object Clone()
        {
            return new SingleShotStraightEnemy();
        }

        public override void Shoot(Transform bulletSpawn)
        {
            if (Time.time - _lastFiredTime >= _config.FireDelay)
            {
                _bullet.Fire(_config, bulletSpawn.position, bulletSpawn.rotation.eulerAngles);
                _lastFiredTime = Time.time;
            }
        }
    }
}