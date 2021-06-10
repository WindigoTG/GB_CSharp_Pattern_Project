using UnityEngine;

namespace ShmupProject
{
    public class PlayerMovementNonPhys : IMovementPlayer
    {
        Transform _playerShip;
        PlayerData _playerData;

        public PlayerMovementNonPhys() { }

        public PlayerMovementNonPhys(Transform playerShip, PlayerData playerData)
        {
            _playerData = playerData;
            _playerShip = playerShip;
        }

        public void SetDependencies(Transform playerShip, PlayerData playerData)
        {
            _playerData = playerData;
            _playerShip = playerShip;
        }

        public void Move(float inputHor, float inputVer, float deltaTime)
        {
            _playerShip.Translate(new Vector3(inputHor, 0, inputVer) * _playerData.Speed * deltaTime);
            ConstrainPlayerPosition();
        }

        private void ConstrainPlayerPosition()
        {
            if (_playerShip.position.x > _playerData.Xbound)
                _playerShip.position = new Vector3(_playerData.Xbound, _playerShip.position.y, _playerShip.position.z);
            if (_playerShip.position.x < -_playerData.Xbound)
                _playerShip.position = new Vector3(-_playerData.Xbound, _playerShip.position.y, _playerShip.position.z);

            if (_playerShip.position.z > _playerData.Zbound)
                _playerShip.position = new Vector3(_playerShip.position.x, _playerShip.position.y, _playerData.Zbound);
            if (_playerShip.position.z < -_playerData.Zbound)
                _playerShip.position = new Vector3(_playerShip.position.x, _playerShip.position.y, -_playerData.Zbound);
        }
    }
}