using UnityEngine;

namespace ShmupProject
{
    public class TestBoss : MonoBehaviour
    {
        [SerializeField] Transform _player;
        BossPart _boss;
        BulletManager _bulletManager;
        PlayerController _playerController;
        IBossMovement _bossMovement;

        private void Awake()
        {
            ServiceLocator.AddService(new WeaponFactory());

            ServiceLocator.AddService(new CollisionManager());
            _bulletManager = new BulletManager();

            ServiceLocator.AddService(new ObjectPoolManager());
            ServiceLocator.AddService(_bulletManager);

            _playerController = new PlayerController(new PlayerFactoryNonPhysical());
        }

        // Start is called before the first frame update
        void Start()
        {
            _boss = new BossCore(transform);

            _boss.AddPart(new BossRotaryWeaponPart(), new BossRotaryWeaponPart());
            _boss.AddPart(new BossSpreadgunWing(), new BossSpreadgunWing());
            _boss.AddPart(new BossRotaryWeaponPart(), new BossSpreadgunWing());

            

            _boss.AddPart(new BossSpreadgunWing(), new BossSpreadgunWing());

            _boss.AddPart(new BossSpreadgunWing(), new BossRotaryWeaponPart());
            _boss.AddPart(new BossRotaryWeaponPart(), new BossSpreadgunWing());
            _boss.AddPart(new BossSpreadgunWing(), new BossRotaryWeaponPart());
            _boss.AddPart(new BossRotaryWeaponPart(), new BossSpreadgunWing());

            transform.Rotate(new Vector3(0, 180, 0));
            Debug.Log($"Left {_boss.WidthLeft}  |  Right {_boss.WidthRight}");
            transform.localScale *= 0.5f;
            Debug.Log(transform.localScale.x);
            Debug.Log($"Left {_boss.WidthLeft}  |  Right {_boss.WidthRight}");
            Debug.Log($"Left {_boss.WidthLeft * transform.localScale.x}   |  Right {_boss.WidthRight * transform.localScale.x}");

            _boss.RecalculateHitPoints();

            ServiceLocator.GetService<CollisionManager>().EnemyHit += CheckIfBossWasHit;
            _bossMovement = new BossMovementRandom(transform);
        }

        private void CheckIfBossWasHit(Transform hit)
        {
            _boss.CheckHit(hit);
        }

        // Update is called once per frame
        void Update()
        {
            _boss.Fire(_playerController.Player);
            _bossMovement.Move(_boss.WidthLeft * 2,
                                _boss.WidthRight * 2,
                                Time.deltaTime);
            _playerController.UpdateRegular(Time.deltaTime);
        }

        private void LateUpdate()
        {
            _bulletManager.UpdateLate(Time.deltaTime);
        }
    }
}