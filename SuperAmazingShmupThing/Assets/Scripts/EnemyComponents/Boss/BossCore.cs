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

            _weapons.Add(new LineStraightEnemy());
            _weapons.Add(new LineStraightEnemy());

            _weaponMounts.Add(_thisPart.Find("WeaponMount1").transform);
            _weaponMounts.Add(_thisPart.Find("WeaponMount2").transform);

            _maxHitPoints = 10;
            _hitPoints = _maxHitPoints;

            GetThisPartMaterial();
        }

        public override void SetPartPosition(Transform parent, bool isRight){}
    }
}