using UnityEngine;

namespace ShmupProject
{
    public sealed class BulletBoundary : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(MagicStrings.Player_Bullet_Tag))
                ObjectPoolManager.GetInstance().PlayerBulletsPool.Push(other.gameObject);

            if (other.CompareTag(MagicStrings.Enemy_Bullet_Tag))
                ObjectPoolManager.GetInstance().EnemyBulletsPool.Push(other.gameObject);
        }
    }
}