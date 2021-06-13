using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class RegularEnemyWaveState : EnemyWaveState
    {
        private List<Enemy> _enemies;
        private MovementFunction _movement;

        private float _posX;
        private float _tgtX;
        private float _tgtZ;

        private float _deltaTime;

        private EnemyFactory _enemyFactory;
        private Transform _targetPlayer;
        private EnemyController _controller;

        #region Spawn Management Logic PlaceHolder
        private bool _isSpawnTime = true;
        private float _defaultSpawnCD = 0.5f;
        private float _currentSpawnCD;
        private int _numberOfEnemies = 1;
        private int _enemiesToSpawnInWave;
        private int _enemiesToDefeat;
        #endregion

        public RegularEnemyWaveState(Transform targetPlayer, EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _targetPlayer = targetPlayer;

            _currentSpawnCD = _defaultSpawnCD;
            _enemies = new List<Enemy>();
            _enemiesToSpawnInWave = _numberOfEnemies;
            _enemiesToDefeat = _enemiesToSpawnInWave;
            GenerateNextWaveParameters();
            _targetPlayer = targetPlayer;
            _enemyFactory = enemyFactory;
        }

        public override void Operate(EnemyController controller, float deltaTime)
        {
            _deltaTime = deltaTime;
            _controller = controller;

            if (_isSpawnTime)
                SpawnEnemies();

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].IsActive)
                {
                    _enemies[i].UpdateRegular(_deltaTime);
                }
            }
        }

        #region Spawn Management Logic PlaceHolder
        private void CountEnemiewsDown()
        {
            _enemiesToDefeat--;
            if (_enemiesToDefeat == 0)
            {
                GenerateNextWaveParameters();
                _enemiesToSpawnInWave = _numberOfEnemies + _controller.WaveNumber;
                _enemiesToDefeat = _enemiesToSpawnInWave;
                _isSpawnTime = true;
                _controller.SetWaveDelay();
            }
        }
        #endregion

        private void SpawnEnemies()
        {
            if (_currentSpawnCD > 0)
                _currentSpawnCD -= _deltaTime;
            else
            {
                if (_enemiesToSpawnInWave > 0)
                {
                    Enemy enemy = null; ;
                    if (_enemies.Count > 0)
                    {
                        enemy = _enemies.Find(x => !x.IsActive);
                    }
                    if (enemy is null)
                    {
                        enemy = _enemyFactory.CreateEnemy();
                    }
                    enemy.ActivateEnemy(new Vector3(_posX, 0, 10), Quaternion.Euler(0, 180, 0));
                    enemy.SetMovementMethod(_movement, _tgtX, _tgtZ);
                    _currentSpawnCD = _defaultSpawnCD;
                    enemy.ShootingTarget = _targetPlayer;
                    enemy.Deactivation += DeactivateEnemy;
                    if (!_enemies.Contains(enemy))
                        _enemies.Add(enemy);

                    _enemiesToSpawnInWave--;
                }
                else
                    _isSpawnTime = false;
            }
        }

        private void DeactivateEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy))
            {
                CountEnemiewsDown();
                var temp = _enemies.Find(x => x == enemy);
                temp.Deactivation -= DeactivateEnemy;
            }

        }

        private void GenerateNextWaveParameters()
        {
            _posX = Random.Range(-5, 5);
            switch (Random.Range(0, 3))
            {
                case 0:
                    {
                        _movement = MovementFunction.Linear;
                        break;
                    }
                case 1:
                    {
                        _movement = MovementFunction.Quadratic;
                        break;
                    }
                case 2:
                    {
                        _movement = MovementFunction.Cubic;
                        break;
                    }
                default:
                    {
                        _movement = MovementFunction.Linear;
                        break;
                    }
            }

            switch (_movement)
            {
                case MovementFunction.Linear:
                    {
                        _tgtX = Random.Range(-4.0f, 4.0f);
                        _tgtZ = Random.Range(-6.0f, 0.0f);
                        break;
                    }
                case MovementFunction.Quadratic:
                    {
                        _tgtX = Random.Range(-3.0f, 3.0f);
                        _tgtZ = Random.Range(-6.0f, 0.0f);
                        break;
                    }
                case MovementFunction.Cubic:
                    {
                        _tgtX = Random.Range(-2.0f, 2.0f);
                        _tgtZ = Random.Range(1f, 10.0f);
                        break;
                    }
            }
        }
    }
}