using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class WeaponTest : MonoBehaviour
    {
        [SerializeField] private BullletConfig _config;
        private BulletManager _bulletManager;
        private IFireable _projectile;
        private float _fireDelay;

        private float _lastFiredTime;

        private void Awake()
        {
            ServiceLocator.AddService(new CollisionManager());
            _bulletManager = new BulletManager();

            ServiceLocator.AddService(new BulletPoolManager());
            ServiceLocator.AddService(_bulletManager);

            //Arc arc = new Arc();
            //Line line = new Line();
            //SingleBullet bullet = new SingleBullet();
            //_projectile = arc.Of(line).Of(bullet);
            _projectile = new SingleBullet(BulletOwner.Enemy).FiredInLine().FiredInArc();
            //_projectile = new SingleBullet().FiredInArc().FiredInLine().FiredInArc().FiredInLine().FiredInArc();
            //_projectile = new SingleBullet().FiredInArc().FiredInArc().FiredInArc().FiredInArc().FiredInLine();
            //_projectile = new SingleBullet(BulletOwner.Enemy);

            _fireDelay = _config.FireDelay;
            _lastFiredTime = Time.time;
        }

        void Update()
        {
            if (Time.time - _lastFiredTime >= _fireDelay)
            {
                Fire();
                _lastFiredTime = Time.time;
                _fireDelay = _config.FireDelay;
            }
        }

        private void LateUpdate()
        {
            _bulletManager.UpdateLate(Time.deltaTime);
        }

        private void Fire()
        {
            _projectile.Fire(_config, transform.position, transform.rotation.eulerAngles);
        }
    }
}