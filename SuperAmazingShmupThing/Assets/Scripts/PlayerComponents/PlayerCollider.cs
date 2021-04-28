using System;
using UnityEngine;

namespace ShmupProject
{
    public class PlayerCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyBullet"))
            {
                Destroy(other.gameObject);
                GetHit?.Invoke();
            }
            if (other.CompareTag("Enemy"))
            {
                GetHit?.Invoke();
            }
        }

        public Action GetHit;
    }
}