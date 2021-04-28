using UnityEngine;

namespace ShmupProject
{
    public interface IWeaponPlayer
    {
        void Shoot(Transform bulletSpawn);
    }
}