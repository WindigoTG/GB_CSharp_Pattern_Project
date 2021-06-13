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

            _weapons.Add(ServiceLocator.GetService<WeaponFactory>().CreateWeapon(EnemyWeaponType.TenLinesSpinning, false));

            _weaponMounts.Add(_thisPart.GetComponentInChildren<Grid>().transform);

            _maxHitPoints = 5;
            _hitPoints = _maxHitPoints;

            GetThisPartMaterial();

            _scoreValue = 750;
        }

        public override void SetPartPosition(Transform parent, bool isRight)
        {
            base.SetPartPosition(parent, isRight);
            _thisPart.GetComponentInChildren<Rigidbody>().AddTorque(Vector3.up * torqueForce * _thisPart.localScale.x);
            if (!isRight)
                foreach (IWeaponEnemy weapon in _weapons)
                    if(weapon is EnemyWeapon)
                        (weapon as EnemyWeapon).InvertAngularSpeed();

        }
    }
}