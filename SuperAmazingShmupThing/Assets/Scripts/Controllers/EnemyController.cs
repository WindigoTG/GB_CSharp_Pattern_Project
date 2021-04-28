using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class EnemyController : IUpdateable
    {
        private List<Enemy> _enemies;
        private MovementFunction _movement;

        private Transform _enemyParent;

        private float _posX;
        private float _tgtX;
        private float _tgtZ;

        private float _deltaTime;

        private Transform _targetPlayer;

        #region Spawn Management Logic PlaceHolder
        private bool _isSpawnTime = true;
        private float _defaultSpawnCD = 0.5f;
        private float _currentSpawnCD;
        private int _numberOfEnemies = 3;
        private int _enemiesToSpawnInWave;
        private int _enemiesToDefeat;
        private int _waveCompleted = 0;
        private bool _isPauseBetweenWaves;
        #endregion

        public EnemyController(Transform targetPlayer)
        {
            _currentSpawnCD = _defaultSpawnCD;
            _enemies = new List<Enemy>();
            _enemiesToSpawnInWave = _numberOfEnemies;
            _enemiesToDefeat = _enemiesToSpawnInWave;
            _enemyParent = new GameObject("Enemies").GetComponent<Transform>();
            GenerateNextWaveParameters();
            InitEnemies();
            _targetPlayer = targetPlayer;
        }

        public void UpdateRegular(float deltaTime)
        {
            _deltaTime = deltaTime;

            if (_isSpawnTime)
                SpawnEnemies();

            if (!_isPauseBetweenWaves)
            {
                for (int i = 0; i < _enemies.Count; i++)
                {
                    if (_enemies[i].gameObject.activeSelf)
                    {
                        _enemies[i].UpdateRegular(_deltaTime);
                        if (_enemies[i].transform.position.z < -10 || _enemies[i].transform.position.z > 11 || 
                            _enemies[i].transform.position.x < -6 || _enemies[i].transform.position.x > 6)
                        {
                            _enemies[i].gameObject.SetActive(false);
                            _enemies[i].GotHit -= DeactivateEnemy;
                            CountEnemiewsDown();
                        }
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
                _isPauseBetweenWaves = true;
                _waveCompleted++;
                GenerateNextWaveParameters();
                _enemiesToSpawnInWave = _numberOfEnemies + _waveCompleted;
                _enemiesToDefeat = _enemiesToSpawnInWave;
                InitEnemies();
                _isSpawnTime = true;
                _isPauseBetweenWaves = false;
            }
        }
        #endregion

        private void InitEnemies()
        {
            while (_enemies.Count < _enemiesToSpawnInWave)
            {
                var enemy = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemy"), new Vector3(_posX, 1, 10), Quaternion.Euler(0, 180, 0), _enemyParent);
                _enemies.Add(enemy.AddComponent<Enemy>());
                enemy.gameObject.SetActive(false);
            }
        }

        private void SpawnEnemies()
        {
            if (_currentSpawnCD > 0)
                _currentSpawnCD -= _deltaTime;
            else
            {
                if (_enemiesToSpawnInWave > 0)
                {
                    var enemy = _enemies[--_enemiesToSpawnInWave];
                    enemy.transform.position = new Vector3(_posX, 1, 10);
                    enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
                    enemy.SetMovementMethod(_movement, _tgtX, _tgtZ);
                    enemy.gameObject.SetActive(true);
                    _currentSpawnCD = _defaultSpawnCD;
                    enemy.ShootingTarget = _targetPlayer;
                    enemy.GotHit += DeactivateEnemy;
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
                temp.gameObject.SetActive(false);
                temp.GotHit -= DeactivateEnemy;
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

            //Debug.Log("posX = " + _posX + ", tgtX = " + _tgtX + ", tgtZ = " + _tgtZ + ", Function = " + _movement);
        }
    }

    public enum MovementFunction
    { 
        Linear,
        Quadratic,
        Cubic
    }
}