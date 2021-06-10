using System;
using UnityEngine;

namespace ShmupProject
{
    public class CollisionManager
    {
        public Action<Transform> EnemyHit;
        public Action PlayerHit;

        public bool CheckCollisions(Vector3 origin, float radius, Vector3 direction, int layerMask)
        {
            RaycastHit[] hits = new RaycastHit[128];
            float maxDistance = direction.magnitude;

            if (Physics.SphereCastNonAlloc(origin, radius, direction.normalized, hits, maxDistance, layerMask, QueryTriggerInteraction.Collide) > 0)
            {
                foreach (var h in hits)
                    if (h.collider != null)
                    {
                        NotifyObservers(h, layerMask);
                        return true;
                    }
            }
                return false;
        }

        void NotifyObservers(RaycastHit hit, int layerMask)
        {
            if (layerMask == LayerMask.GetMask(Constants.PlayerLayer))
            {
                PlayerHit?.Invoke();
            }
            if (layerMask == LayerMask.GetMask(Constants.EnemyLayer))
            {
                EnemyHit?.Invoke(hit.collider.transform);
            }
        }
    }
}