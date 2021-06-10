using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class BossCore : BossPart
    {
        private string partPath = "Prefabs/Boss/Boss_Core";

        public BossCore(Transform parent)
        {
            _weapons = new List<IWeaponEnemy>();
            _weaponMounts = new List<Transform>();
            _partsMaterial = new List<Material>();_partsMaterial = new List<Material>();

            _thisPart = Object.Instantiate(Resources.Load<GameObject>(partPath)).transform;
            _thisPart.position = parent.position;
            _thisPart.parent = parent;

            foreach (var wm in _thisPart.GetComponentsInChildren<Grid>())
                _weaponMounts.Add(wm.transform);

            foreach (var wm in _weaponMounts)
            {
                IWeaponEnemy weapon = ServiceLocator.GetService<WeaponFactory>().CreateWeapon(EnemyWeaponType.LineStraight, true);
                LeadTargeting targeting = new LeadTargeting();
                targeting.IsPredicting = true;
                AdwancedTrackingWeaponProxy proxy = new AdwancedTrackingWeaponProxy(weapon, targeting);
                _weapons.Add(proxy);

                //_weapons.Add(ServiceLocator.GetService<WeaponFactory>().CreateWeapon(EnemyWeaponType.LineStraight, false));
            }

            _maxHitPoints = 10;
            _hitPoints = _maxHitPoints;

            GetThisPartMaterial();
        }

        public override void SetPartPosition(Transform parent, bool isRight){}
    }
}