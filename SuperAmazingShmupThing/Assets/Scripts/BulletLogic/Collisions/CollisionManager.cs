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
            RaycastHit hit;
            float maxDistance = direction.magnitude;

            if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.Collide))
            {
                NotifyObservers(hit, layerMask);
                return true;
            }
                return false;
        }

        void NotifyObservers(RaycastHit hit, int layerMask)
        {
            if (layerMask == LayerMask.GetMask(MagicStrings.PlayerLayer))
            {
                PlayerHit?.Invoke();
            }
            if (layerMask == LayerMask.GetMask(MagicStrings.EnemyLayer))
            {
                EnemyHit?.Invoke(hit.collider.transform);
            }
        }
    }
}