using UnityEngine;

namespace ShmupProject
{
    public sealed class LineStraightEnemy : EnemyWeapon
    {
        public LineStraightEnemy()
        {
            float bulletSpeed = 2.0f;
            float fireDelay = 2.0f;
            float bulletLifeTime = 5.0f;
            int bulletCount = 5;
            float deltaSpeed = 1;

            _bullet = new SingleBullet(BulletOwner.Enemy).FiredInLine();
            _config = new BullletConfig(bulletSpeed, 0, bulletLifeTime, fireDelay, bulletCount, deltaSpeed, 1, 0, 0);
         }

        public override object Clone()
        {
            return new LineStraightEnemy();
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