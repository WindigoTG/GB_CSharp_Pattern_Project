using System;
using UnityEngine;

namespace ShmupProject
{
    public abstract class EnemyWeapon : IWeaponEnemy, ICloneable
    {
        protected BullletConfig _config;
        protected IFireable _bullet;
        protected float _lastFiredTime = 0;

        public abstract object Clone();
        public abstract void Shoot(Transform bulletSpawn);
    }
}