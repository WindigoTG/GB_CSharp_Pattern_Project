using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public sealed class EnemyController : IUpdateable
    {
        private List<Enemy> _enemies;
        private MovementFunction _movement;

        private float _posX;
        private float _tgtX;
        private float _tgtZ;

        private float _boundX = 6;
        private float _boundZ = 10;

        private float _deltaTime;

        private Transform _targetPlayer;
        private EnemyPool _enemyPool;


        #region Spawn Management Logic PlaceHolder
        private bool _isSpawnTime = true;
        private float _defaultSpawnCD = 0.5f;
        private float _currentSpawnCD;
        private int _numberOfEnemies = 1;
        private int _enemiesToSpawnInWave;
        private int _enemiesToDefeat;
        private int _waveCompleted = 0;
        #endregion

        public EnemyController(Transform targetPlayer, EnemyPool enemyPool)
        {
            _currentSpawnCD = _defaultSpawnCD;
            _enemies = new List<Enemy>();
            _enemiesToSpawnInWave = _numberOfEnemies;
            _enemiesToDefeat = _enemiesToSpawnInWave;
            GenerateNextWaveParameters();
            _targetPlayer = targetPlayer;
            _enemyPool = enemyPool;
        }

        public void UpdateRegular(float deltaTime)
        {
            _deltaTime = deltaTime;

            if (_isSpawnTime)
                SpawnEnemies();

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].gameObject.activeSelf)
                {
                    _enemies[i].UpdateRegular(_deltaTime);
                    if (_enemies[i].transform.position.z < -_boundZ || _enemies[i].transform.position.z > _boundZ || 
                        _enemies[i].transform.position.x < -_boundX || _enemies[i].transform.position.x > _boundX)
                    {
                        _enemyPool.Push(_enemies[i]);

                        _enemies[i].GotHit -= DeactivateEnemy;
                        CountEnemiewsDown();
                    }
                }
            }
        }

        #region Spawn Management Logic PlaceHolder
        private void CountEnemiewsDown()
        {
            _enemiesToDefeat--;
            if (_enemiesToDefeat == 0)
            {
                _waveCompleted++;
                GenerateNextWaveParameters();
                _enemiesToSpawnInWave = _numberOfEnemies + _waveCompleted;
                _enemiesToDefeat = _enemiesToSpawnInWave;
                _isSpawnTime = true;
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
                    var enemy = _enemyPool.Pop();
                    enemy.transform.position = new Vector3(_posX, 1, 10);
                    enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
                    enemy.SetMovementMethod(_movement, _tgtX, _tgtZ);
                    _currentSpawnCD = _defaultSpawnCD;
                    enemy.ShootingTarget = _targetPlayer;
                    enemy.GotHit += DeactivateEnemy;
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
                temp.GotHit -= DeactivateEnemy;
                _enemyPool.Push(temp);
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

    public enum MovementFunction
    { 
        Linear,
        Quadratic,
        Cubic
    }
}