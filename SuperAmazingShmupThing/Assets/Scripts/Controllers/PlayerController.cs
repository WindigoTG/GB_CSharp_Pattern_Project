using UnityEngine;

namespace ShmupProject
{
    public sealed class PlayerController : IUpdateable
    {
        private Player _player;

        private PlayerFactoryNonPhysical _playerFactory;

        private float inputHor;
        private float inputVer;

        private float _shootingCDtime = 0.5f;
        private float _shootingCD;

        public PlayerController(PlayerFactoryNonPhysical playerFactory)
        {
            _playerFactory = playerFactory;
            _player = _playerFactory.CreatePlayer();

            _player.Collider.GetHit += _player.Health.TakeHit;

            _shootingCD = _shootingCDtime;
        }

        public void UpdateRegular(float deltaTime)
        {
            inputHor = Input.GetAxis(MagicStrings.Input_Axis_Horizontal);
            inputVer = Input.GetAxis(MagicStrings.Input_Axis_Vertical);
            _player.Movement.Move(inputHor, inputVer, deltaTime);

            if (_shootingCD > 0)
                _shootingCD -= deltaTime;

            if (Input.GetAxis(MagicStrings.Input_Axis_Fire) != 0 && _shootingCD <= 0)
            {
                _player.Weapon.Shoot(_player.Transform);
                _shootingCD = _shootingCDtime;
            }
        }

        public Transform Player => _player.Transform;
    }
}