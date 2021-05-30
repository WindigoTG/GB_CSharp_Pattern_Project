using UnityEngine;

namespace ShmupProject
{
    public sealed class SingleShotStraightEnemy : IWeaponEnemy
    {
        private float _bulletSpeed = 10f;

        public void Shoot(Transform bulletSpawn)
        {
            var bullet = ObjectPoolManager.GetInstance().EnemyBulletsPool.Pop();
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * _bulletSpeed, ForceMode.VelocityChange);
        }
    }
}