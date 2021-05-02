using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public sealed class GameController : MonoBehaviour
    {
        private List<IUpdateable> _updatables;
        private PlayerController _playerController;
        private EnemyController _enemyController;

        private void Awake()
        {
            _updatables = new List<IUpdateable>();
            _playerController = new PlayerController(new PlayerFactoryNonPhysical());
            _updatables.Add(_playerController);
            _enemyController = new EnemyController(_playerController.Player, 
                                                    new EnemyPool(new EnemyFactory(new SingleShotStraightEnemy())));
            _updatables.Add(_enemyController);
        }

        void Update()
        {
            if (_updatables.Count > 0)
                foreach (IUpdateable u in _updatables)
                    u.UpdateRegular(Time.deltaTime);
        }
    }
}