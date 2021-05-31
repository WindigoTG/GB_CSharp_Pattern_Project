using UnityEngine;

namespace ShmupProject
{
    public sealed class SingleShotPlayer : IWeaponPlayer
    {
        private float _bulletSpeed = 50f;

        public void Shoot(Transform bulletSpawn)
        {
            var bullet = ServiceLocator.GetService<BulletPoolManager>().PlayerBulletsPool.Pop();
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * _bulletSpeed, ForceMode.VelocityChange);
        }
    }
}