using UnityEngine;

namespace ShmupProject
{
    public class SingleShotStraightEnemy : IWeaponEnemy
    {
        private GameObject _enemyBulletPrefab;
        private float _bulletSpeed = 10f;

        public SingleShotStraightEnemy()
        {
            _enemyBulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet_Enemy");
        }

        public void Shoot(Transform bulletSpawn)
        {
            var bullet = GameObject.Instantiate(_enemyBulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.AddComponent<Rigidbody>().useGravity = false;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * _bulletSpeed, ForceMode.VelocityChange);
        }
    }
}