using System;
using UnityEngine;

namespace ShmupProject
{
    public sealed class Enemy : MonoBehaviour, IUpdateable
    {
        private IMovementEnemy _movement;
        private IWeaponEnemy _weapon;
        private Transform _weaponMount;
        private Transform _targetPlayer;

        private float _shootingCDtime = 2.0f;
        private float _shootingCD;

       public Transform ShootingTarget
        {
            set { _targetPlayer = value; }
        }

        public void SetMovementMethod(MovementFunction movement, float tgtX = 0, float tgtZ = -6)
        {
            switch (movement)
            {
                case MovementFunction.Linear:
                    {
                        _movement = new EnemyMovementLinear(transform, tgtX, tgtZ);
                        break;
                    }
                case MovementFunction.Quadratic:
                    {
                        _movement = new EnemyMovementQuadratic(transform, tgtX, tgtZ);
                        break;
                    }
                case MovementFunction.Cubic:
                    {
                        _movement = new EnemyMovementCubic(transform, tgtX, tgtZ);
                        break;
                    }
                default:
                    {
                        _movement = new EnemyMovementLinear(transform, tgtX, tgtZ);
                        break;
                    }
            }
        }

        void Awake()
        {
            _shootingCD = _shootingCDtime;
            _weaponMount = new GameObject("WeaponMount").transform;
            _weaponMount.transform.parent = transform;
            _weaponMount.position = transform.position;
        }

        public void SetWeapon(IWeaponEnemy weapon)
        {
            _weapon = weapon;
        }

        public void UpdateRegular(float deltaTime)
        {
            if (_movement != null)
            _movement.Move(deltaTime);
            TrackPlayer();
            Shoot(deltaTime);
        }

        private void Shoot(float deltaTime)
        {
            if (_shootingCD > 0)
                _shootingCD -= deltaTime;
            else
            {
                if (_weapon != null)
                    _weapon.Shoot(_weaponMount);
                _shootingCD = _shootingCDtime;
            }
        }

        private void TrackPlayer()
        {
            Quaternion rotation = new Quaternion();
            rotation.SetLookRotation(_targetPlayer.position - _weaponMount.position, Vector3.up);
            _weaponMount.rotation = rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(MagicStrings.Player_Bullet_Tag))
            {
                ServiceLocator.GetService<BulletPoolManager>().PlayerBulletsPool.Push(other.gameObject);
                GotHit?.Invoke(this);
            }
        }

        public Action<Enemy> GotHit;
    }
}