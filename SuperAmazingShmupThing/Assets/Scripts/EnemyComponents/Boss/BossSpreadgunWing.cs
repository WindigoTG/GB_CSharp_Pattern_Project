using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{ 
public class BossSpreadgunWing : BossPart
    {
        private string partPath = "Prefabs/Boss/Boss_Section_Wing";

        public BossSpreadgunWing()
        {
            _weapons = new List<IWeaponEnemy>();
            _weaponMounts = new List<Transform>();
            _partsMaterial = new List<Material>();

            _thisPart = Object.Instantiate(Resources.Load<GameObject>(partPath)).transform;

            _weapons.Add(ServiceLocator.GetService<WeaponFactory>().CreateWeapon(EnemyWeaponType.ThreeLinesSpreadgun, true));

            _weaponMounts.Add(_thisPart.GetComponentInChildren<Grid>().transform);

            _maxHitPoints = 5;
            _hitPoints = _maxHitPoints;

            GetThisPartMaterial();
        }
    }
}