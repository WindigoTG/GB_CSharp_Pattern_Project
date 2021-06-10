using UnityEngine;

namespace ShmupProject
{
    public class Player
    {
        private PlayerData _playerData;
        private Transform _transform;
        private IMovementPlayer _movement;
        private IWeaponPlayer _weapon;
        private PlayerHealth _playerHealth;

        public Player(PlayerData playerData, IMovementPlayer movement, IWeaponPlayer weapon)
        {
            _playerData = playerData;
            _movement =  movement;
            _weapon = weapon;

            _playerHealth = new PlayerHealth();
            _transform = GameObject.Instantiate(_playerData.Prefab, _playerData.Position, Quaternion.identity).transform;

            _movement.SetDependencies(_transform, _playerData);
            ServiceLocator.GetService<CollisionManager>().PlayerHit += _playerHealth.TakeHit;
        }

        public Transform Transform => _transform;
        public IWeaponPlayer Weapon => _weapon;
        public IMovementPlayer Movement => _movement;
        public PlayerHealth Health => _playerHealth;

        public void SetMovement(IMovementPlayer movement)
        {
            _movement = movement;
        }

        public void SetWeapon(IWeaponPlayer weapon)
        {
            _weapon = weapon;
        }
    }
}