using System;
using UnityEngine;

namespace ShmupProject
{
    public sealed class PlayerCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(MagicStrings.Enemy_Bullet_Tag))
            {
                ServiceLocator.GetService<BulletPoolManager>().EnemyBulletsPool.Push(other.gameObject);
                GetHit?.Invoke();
            }
            if (other.CompareTag(MagicStrings.Enemy))
            {
                GetHit?.Invoke();
            }
        }

        public Action GetHit;
    }
}