using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public sealed class GameController : MonoBehaviour
    {
        private List<IUpdateable> _updatables;
        private List<ILateUpdateable> _lateUpdatables;
        private PlayerController _playerController;
        private EnemyController _enemyController;
        private BulletManager _bulletManager;
        private ScoreTracker _scoreTracker;

        private void Awake()
        {
            _updatables = new List<IUpdateable>();
            _lateUpdatables = new List<ILateUpdateable>();

            ServiceLocator.AddService(new CollisionManager());

            _bulletManager = new BulletManager();
            ServiceLocator.AddService(_bulletManager);
            _lateUpdatables.Add(_bulletManager);

            ServiceLocator.AddService(new ObjectPoolManager());
            
            _playerController = new PlayerController(new PlayerFactoryNonPhysical());
            _updatables.Add(_playerController);

            ServiceLocator.AddService(new WeaponFactory());
            _enemyController = new EnemyController(_playerController.Player, new EnemyFactory(), new BossFactory());
            _updatables.Add(_enemyController);

            _scoreTracker = new ScoreTracker(_enemyController);
            ServiceLocator.AddService(_scoreTracker);
            _updatables.Add(_scoreTracker);
        }

        void Update()
        {
            if (_updatables.Count > 0)
                foreach (IUpdateable u in _updatables)
                    u.UpdateRegular(Time.deltaTime);
        }

        private void LateUpdate()
        {
            if (_lateUpdatables.Count > 0)
                foreach (ILateUpdateable u in _lateUpdatables)
                    u.UpdateLate(Time.deltaTime);
        }
    }
}
