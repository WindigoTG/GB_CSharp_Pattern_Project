using UnityEngine;

namespace ShmupProject
{
    public class PlayerFactoryNonPhysical
    {
        private IWeaponPlayer _weapon;

        public PlayerFactoryNonPhysical() 
        {
            _weapon = new SingleShotPlayer();
        }

        public PlayerFactoryNonPhysical(IWeaponPlayer weapon)
        {
            _weapon = weapon;
        }

        public Player CreatePlayer()
        {
            return new Player(Resources.Load<PlayerData>(Constants.Player_Data), new PlayerMovementNonPhys(), _weapon);
        }

        public Player CreatePlayerPhysicalMovement()
        {
            return new Player(Resources.Load<PlayerData>(Constants.Player_Data), new PlayerMovementPhysics(), _weapon);
        }
    }
}