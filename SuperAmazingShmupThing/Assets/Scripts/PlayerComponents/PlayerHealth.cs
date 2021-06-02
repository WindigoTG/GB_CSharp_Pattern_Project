using UnityEngine;

namespace ShmupProject
{
    public sealed class PlayerHealth
    {
        private float _hp = 100;

        public void TakeHit()
        {
            Debug.LogWarning($"Got hit at {Time.time}");
        }
    }
}