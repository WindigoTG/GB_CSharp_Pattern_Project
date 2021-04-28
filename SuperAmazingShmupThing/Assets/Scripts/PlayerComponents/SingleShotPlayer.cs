using UnityEngine;

namespace ShmupProject
{
    public class SingleShotPlayer : IWeaponPlayer
    {
        private GameObject _playerBulletPrefab;
        private float _bulletSpeed = 50f;

        public SingleShotPlayer()
        {
            _playerBulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet_Player");
        }

        public void Shoot(Transform bulletSpawn)
        {
            var bullet = GameObject.Instantiate(_playerBulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.AddComponent<Rigidbody>().useGravity = false;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * _bulletSpeed, ForceMode.VelocityChange);
        }
    }
}