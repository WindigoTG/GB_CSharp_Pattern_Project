using UnityEngine;

namespace ShmupProject
{
    public sealed class BulletBoundary : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(MagicStrings.Player_Bullet_Tag))
                ServiceLocator.GetService<BulletPoolManager>().PlayerBulletsPool.Push(other.gameObject);

            if (other.CompareTag(MagicStrings.Enemy_Bullet_Tag))
                ServiceLocator.GetService<BulletPoolManager>().EnemyBulletsPool.Push(other.gameObject);
        }
    }
}