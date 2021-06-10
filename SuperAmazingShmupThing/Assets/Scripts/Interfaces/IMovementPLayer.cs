using UnityEngine;

namespace ShmupProject
{
    public interface IMovementPlayer
    {
        void Move(float inputHor, float inputVer, float deltaTime);
        void SetDependencies(Transform playerShip, PlayerData playerData);
    }
}