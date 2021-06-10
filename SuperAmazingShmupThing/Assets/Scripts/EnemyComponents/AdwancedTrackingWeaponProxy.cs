using UnityEngine;

namespace ShmupProject
{
    public class AdwancedTrackingWeaponProxy : IWeaponEnemy
    {
        IWeaponEnemy _weapon;
        LeadTargeting _targeting;

        public AdwancedTrackingWeaponProxy(IWeaponEnemy weapon, LeadTargeting targeting)
        {
            _weapon = weapon;
            _targeting = targeting;
        }

        public void Shoot(Transform bulletSpawn, Vector3 targetPosition)
        {
            _targeting.OriginPosition = bulletSpawn.position;
            _targeting.TargetPosition = targetPosition;
            _weapon.Shoot(bulletSpawn, _targeting.TargetPosition);
        }
    }
}