using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShmupProject
{
    public sealed class Enemy : IUpdateable
    {
        private IMovementEnemy _movement;
        private IWeaponEnemy _weapon;
        private Transform _enemy;
        private Transform _weaponMount;
        private Transform _targetPlayer;

        private float _screenBoundOffset = 2.0f;

        public Action<Enemy> Deactivation;

        private ObjectPool _enemyPool;
        private ScoreTracker _scoreTracker;

        LeadTargeting _targeting;

        private int _scoreValue = 100;

        //private GameObject _receiver;

        public Enemy()
        {
            _enemyPool = ServiceLocator.GetService<ObjectPoolManager>().EnemyPool;
            _scoreTracker = ServiceLocator.GetService<ScoreTracker>();
            //_receiver = UnityEngine.Object.FindObjectOfType<SCoreMessageReceiver>().gameObject; 
        }

        public Transform ShootingTarget
        {
            set { _targetPlayer = value; }
        }

        public bool IsActive => _enemy != null;

        public void ActivateEnemy(Vector3 position, Quaternion rotation)
        {
            _enemy = _enemyPool.Pop().transform;
            _weaponMount = _enemy.GetComponentInChildren<Grid>().transform;
            _enemy.position = position;
            _enemy.rotation = rotation;

            int random = UnityEngine.Random.Range(0, 2);
            if (random == 0)
                _targeting.IsPredicting = false;
            else
                _targeting.IsPredicting = true;
        }

        public void SetMovementMethod(MovementFunction movement, float tgtX = 0, float tgtZ = -6)
        {
            ServiceLocator.GetService<CollisionManager>().EnemyHit += CheckHit;
            switch (movement)
            {
                case MovementFunction.Linear:
                    {
                        _movement = new EnemyMovementLinear(_enemy.transform, tgtX, tgtZ);
                        break;
                    }
                case MovementFunction.Quadratic:
                    {
                        _movement = new EnemyMovementQuadratic(_enemy.transform, tgtX, tgtZ);
                        break;
                    }
                case MovementFunction.Cubic:
                    {
                        _movement = new EnemyMovementCubic(_enemy.transform, tgtX, tgtZ);
                        break;
                    }
                default:
                    {
                        _movement = new EnemyMovementLinear(_enemy.transform, tgtX, tgtZ);
                        break;
                    }
            }
        }

        public void SetWeapon(IWeaponEnemy weapon)
        {
            //_weapon = weapon;

            _targeting = new LeadTargeting();
            AdwancedTrackingWeaponProxy proxy = new AdwancedTrackingWeaponProxy(weapon, _targeting);
            _weapon = proxy;
        }

        public void UpdateRegular(float deltaTime)
        {
            _movement?.Move(deltaTime);
            Shoot();

            if (_enemy.position.z < -Constants.ScreenBoundZ - _screenBoundOffset || _enemy.position.z > Constants.ScreenBoundZ + _screenBoundOffset ||
                _enemy.position.x < -Constants.ScreenBoundX - _screenBoundOffset || _enemy.position.x > Constants.ScreenBoundX + _screenBoundOffset)
                DeactivateEnemy();
        }

        private void Shoot()
        {
             _weapon.Shoot(_weaponMount, _targetPlayer.position);
        }

        private void CheckHit(Transform hit)
        {
            Transform[] parts = _enemy.GetComponentsInChildren<Transform>();
            foreach (Transform t in parts)
                if (t == hit)
                {
                    //ExecuteEvents.Execute<IScoreMessageReseiver>(_receiver, null, (x,y) => x.AddScore(_scoreValue));
                    _scoreTracker.AddScore(_scoreValue);
                    _enemy.position = new Vector3(Constants.ScreenBoundX + 1, 0, Constants.ScreenBoundZ + 1);
                    
                    DeactivateEnemy();
                    break;
                }
        }

        private void DeactivateEnemy()
        {
            ServiceLocator.GetService<CollisionManager>().EnemyHit -= CheckHit;
            Deactivation?.Invoke(this);
            _enemyPool.Push(_enemy.gameObject);
            _enemy = null;
            _weaponMount = null;
        }
    }
}