using UnityEngine;

namespace ShmupProject
{
    public class PlayerFactoryPhysical
    {
        private IWeaponPlayer _weapon;

        public PlayerFactoryPhysical() 
        {
            _weapon = new SingleShotPlayer();
        }

        public PlayerFactoryPhysical(IWeaponPlayer weapon)
        {
            _weapon = weapon;
        }

        public Player CreatePlayer()
        {
            return new Player(Resources.Load<PlayerData>(Constants.Player_Data), new PlayerMovementPhysics(), _weapon);
        }
    }
}