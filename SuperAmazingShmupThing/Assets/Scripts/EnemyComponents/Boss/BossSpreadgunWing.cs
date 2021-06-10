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

            _weapons.Add(new ThreeLinesSpreadgunEnemy());

            _weaponMounts.Add(_thisPart.Find("WeaponMount").transform);

            _maxHitPoints = 5;
            _hitPoints = _maxHitPoints;

            GetThisPartMaterial();
        }

        public override void Fire(Transform player)
        {
            foreach (Transform wm in _weaponMounts)
            {
                Quaternion rotation = new Quaternion();
                rotation.SetLookRotation(player.position - wm.position, Vector3.up);
                wm.rotation = rotation;
            }
            base.Fire(player);
        }
    }
}