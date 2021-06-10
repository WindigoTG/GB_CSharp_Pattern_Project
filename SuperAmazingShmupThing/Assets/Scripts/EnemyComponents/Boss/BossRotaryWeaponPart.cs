using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class BossRotaryWeaponPart : BossPart
    {
        private string partPath = "Prefabs/Boss/Boss_Section_Rotary";
        private float torqueForce = 50;

        public BossRotaryWeaponPart()
        {
            _weapons = new List<IWeaponEnemy>();
            _weaponMounts = new List<Transform>();
            _partsMaterial = new List<Material>();

            _thisPart = Object.Instantiate(Resources.Load<GameObject>(partPath)).transform;

            _weapons.Add(new TenLinesSpinningEnemy());

            _weaponMounts.Add(_thisPart.Find("WeaponMount").transform);

            _maxHitPoints = 5;
            _hitPoints = _maxHitPoints;

            GetThisPartMaterial();
        }

        public override void SetPartPosition(Transform parent, bool isRight)
        {
            base.SetPartPosition(parent, isRight);
            _thisPart.GetComponentInChildren<Rigidbody>().AddTorque(Vector3.up * torqueForce * _thisPart.localScale.x);
            if (!isRight)
                foreach (IWeaponEnemy weapon in _weapons)
                    if(weapon is TenLinesSpinningEnemy)
                        (weapon as TenLinesSpinningEnemy).InvertSpin();

        }
    }
}