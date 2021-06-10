using UnityEngine;

namespace ShmupProject
{
    public sealed class ThreeLinesSpreadgunEnemy : EnemyWeapon
    {
        public ThreeLinesSpreadgunEnemy()
        {
            float bulletSpeed = 1.0f;
            float angularSpeed = 0.0f;
            float fireDelay = 3.5f;
            float bulletLifeTime = 5.0f;
            int bulletCount = 5;
            float deltaSpeed = 1;
            int lineCount = 3;
            float arcAngle = 30;
            float initialRadius = 0.2f;

            _bullet = new SingleBullet(BulletOwner.Enemy).FiredInLine().FiredInArc();
            _config = new BullletConfig(bulletSpeed, angularSpeed, bulletLifeTime, fireDelay, bulletCount, deltaSpeed, lineCount, arcAngle, initialRadius);
         }

        public override object Clone()
        {
            return new ThreeLinesSpreadgunEnemy();
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