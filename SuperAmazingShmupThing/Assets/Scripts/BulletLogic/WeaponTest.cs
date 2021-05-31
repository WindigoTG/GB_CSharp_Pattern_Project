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

        // Start is called before the first frame update
        void Start()
        {
            _bulletManager = new BulletManager();

            Arc arc = new Arc();
            Line line = new Line();
            SingleBullet bullet = new SingleBullet();
            _projectile = arc.Of(line).Of(bullet);

            _fireDelay = _config.FireDelay;
        }

        // Update is called once per frame
        void Update()
        {
            if (_fireDelay > 0)
                _fireDelay -= Time.deltaTime;
            else
            {
                Fire();
                _fireDelay = _config.FireDelay;
            }
        }

        private void LateUpdate()
        {
            _bulletManager.UpdateLate(Time.deltaTime);
        }

        private void Fire()
        {
            _projectile.Fire(_config, transform.position, transform.rotation.eulerAngles, _bulletManager);
        }
    }
}