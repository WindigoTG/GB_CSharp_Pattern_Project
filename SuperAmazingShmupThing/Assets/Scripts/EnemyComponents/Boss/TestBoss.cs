using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class TestBoss : MonoBehaviour
    {
        [SerializeField] Transform _player;
        BossPart _boss;
        BulletManager _bulletManager;
        PlayerController _playerController;

        private void Awake()
        {
            ServiceLocator.AddService(new CollisionManager());
            _bulletManager = new BulletManager();

            ServiceLocator.AddService(new BulletPoolManager());
            ServiceLocator.AddService(_bulletManager);

            _playerController = new PlayerController(new PlayerFactoryNonPhysical());
        }

        // Start is called before the first frame update
        void Start()
        {
            _boss = new BossCore(transform);

            _boss.AddPart(new BossRotaryWeaponPart(), new BossRotaryWeaponPart());
            _boss.AddPart(new BossSpreadgunWing(), new BossSpreadgunWing());
            //_boss.AddPart(new BossSpreadgunWing(), new BossSpreadgunWing());

            //_boss.AddPart(new BossSpreadgunWing(), new BossRotaryWeaponPart());
            //_boss.AddPart(new BossRotaryWeaponPart(), new BossSpreadgunWing());

            transform.localScale *= 0.3f;
            transform.Rotate(new Vector3(0, 180, 0));

            ServiceLocator.GetService<CollisionManager>().EnemyHit += CheckIsBossWasHit;
        }

        private void CheckIsBossWasHit(Transform hit)
        {
            _boss.CheckHit(hit);
        }

        // Update is called once per frame
        void Update()
        {
            _boss.Fire(_playerController.Player);
            _playerController.UpdateRegular(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _bulletManager.UpdateLate(Time.deltaTime);
        }
    }
}