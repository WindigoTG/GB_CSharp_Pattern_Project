using UnityEngine;

namespace ShmupProject
{
    public sealed class TenLinesSpinningEnemy : EnemyWeapon
    {
        public TenLinesSpinningEnemy()
        {
            float bulletSpeed = 1.0f;
            float angularSpeed = -50.0f;
            float fireDelay = 10f;
            float bulletLifeTime = 7.1f;
            int bulletCount = 5;
            float deltaSpeed = 1;
            int lineCount = 10;
            float arcAngle = 324;
            float initialRadius = 0.5f;

            _bullet = new SingleBullet(BulletOwner.Enemy).FiredInLine().FiredInArc();
            _config = new BullletConfig(bulletSpeed, angularSpeed, bulletLifeTime, fireDelay, bulletCount, deltaSpeed, lineCount, arcAngle, initialRadius);
         }

        public override object Clone()
        {
            return new TenLinesSpinningEnemy();
        }

        public void InvertSpin() => _config.BulletAngularSpeed = -_config.BulletAngularSpeed * 180/Mathf.PI ;

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