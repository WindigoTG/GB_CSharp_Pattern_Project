using UnityEngine;

namespace ShmupProject
{
    public class EnemyWeaponNonTracking : EnemyWeapon
    {
        public EnemyWeaponNonTracking(BullletConfig config, IFireable bullet) : base(config, bullet) { }

        public override object Clone()
        {
            return new EnemyWeaponNonTracking(_config, _bullet);
        }

        public override void Shoot(Transform bulletSpawn, Vector3 targetPosition)
        {
            if (Time.time - _lastFiredTime >= _config.FireDelay)
            {
                _bullet.Fire(_config, bulletSpawn.position, bulletSpawn.rotation.eulerAngles);
                _lastFiredTime = Time.time;
            }
        }
    }
}