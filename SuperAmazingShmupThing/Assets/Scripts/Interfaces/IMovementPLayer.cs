using UnityEngine;

namespace ShmupProject
{
    public interface IMovementPLayer
    {
        void Move(float inputHor, float inputVer, float deltaTime);
        void SetDependencies(Transform playerShip, PlayerData playerData);
    }
}