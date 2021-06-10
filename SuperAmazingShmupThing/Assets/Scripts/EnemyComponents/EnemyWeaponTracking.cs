using UnityEngine;

namespace ShmupProject
{
    public class EnemyWeaponTracking : EnemyWeapon
    {
        public EnemyWeaponTracking(BullletConfig config, IFireable bullet) : base(config, bullet) { }

        public override object Clone()
        {
            return new EnemyWeaponTracking(_config, _bullet);
        }

        public override void Shoot(Transform bulletSpawn, Vector3 targetPosition)
        {
            if (Time.time - _lastFiredTime >= _config.FireDelay)
            {
                Quaternion rotation = new Quaternion();
                rotation.SetLookRotation(targetPosition - bulletSpawn.position, Vector3.up);
                _bullet.Fire(_config, bulletSpawn.position, rotation.eulerAngles);
                _lastFiredTime = Time.time;
            }
        }
    }
}