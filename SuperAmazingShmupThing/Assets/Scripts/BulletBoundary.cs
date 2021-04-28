using UnityEngine;

namespace ShmupProject
{
    public class BulletBoundary : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
                Destroy(other.gameObject);
        }
    }
}