using UnityEngine;

namespace ShmupProject
{
    public class PlayerController : IUpdateable
    {
        private PlayerData _playerData;
        private GameObject _player;
        private Transform _transform;
        private IMovementPLayer _movement;
        private IWeaponPlayer _weapon;
        private PlayerHealth _playerHealth;

        private float inputHor;
        private float inputVer;

        private float _shootingCDtime = 0.5f;
        private float _shootingCD;

        public PlayerController()
        {
            _playerData = Resources.Load<PlayerData>("Data/PlayerData");
            _player = GameObject.Find("Player");
            _transform = _player.transform;
            _transform.position = _playerData.Position;
            _playerHealth = new PlayerHealth();

            _player.GetComponentInChildren<PlayerCollider>().GetHit += _playerHealth.TakeHit;

            _movement = new PlayerMovementNonPhys(_transform, _playerData);
            //_movement = new PlayerMovementPhysics(_transform, _playerData);

            _weapon = new SingleShotPlayer();

            _shootingCD = _shootingCDtime;
        }

        public void UpdateRegular(float deltaTime)
        {
            inputHor = Input.GetAxis("Horizontal");
            inputVer = Input.GetAxis("Vertical");
            _movement.Move(inputHor, inputVer, deltaTime);

            if (_shootingCD > 0)
                _shootingCD -= deltaTime;

            if (Input.GetAxis("Fire1") != 0 && _shootingCD <= 0)
            {
                _weapon.Shoot(_transform);
                _shootingCD = _shootingCDtime;
            }
        }

        public Transform Player => _player.transform;
    }
}