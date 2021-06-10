using UnityEngine;

namespace ShmupProject
{
    public sealed class SingleShotPlayer : IWeaponPlayer
    {
        BullletConfig _config;
        IFireable _bullet;
        float _lastFiredTime = 0;

        public SingleShotPlayer()
        {
            float bulletSpeed = 15.0f;
            float fireDelay = .5f;
            float bulletLifeTime = 5.0f;

            _bullet = new SingleBullet(BulletOwner.Player);
            _config = new BullletConfig(bulletSpeed, 0, bulletLifeTime, fireDelay, 1, 1, 1, 0, 0);
        }

        public void Shoot(Transform bulletSpawn)
        {
            if ((Time.time - _lastFiredTime >= _config.FireDelay) || _lastFiredTime == 0)
            {
                _bullet.Fire(_config, bulletSpawn.position, bulletSpawn.rotation.eulerAngles);
                _lastFiredTime = Time.time;
            }
        }
    }
}