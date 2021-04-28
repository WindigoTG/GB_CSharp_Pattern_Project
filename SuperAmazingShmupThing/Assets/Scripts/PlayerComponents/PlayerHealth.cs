using UnityEngine;

namespace ShmupProject
{
    public class PlayerHealth : MonoBehaviour
    {
        private float _hp = 100;

        public void TakeHit()
        {
            Debug.LogWarning("Got hit");
        }
    }
}