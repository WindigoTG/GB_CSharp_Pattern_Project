using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class CollisionTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.layer = LayerMask.NameToLayer(MagicStrings.PlayerLayer);
            ServiceLocator.GetService<CollisionManager>().PlayerHit += GotHit;
        }

        private void GotHit()
        {
            Debug.LogWarning($"Got hit at {Time.time}");
        }
    }
}