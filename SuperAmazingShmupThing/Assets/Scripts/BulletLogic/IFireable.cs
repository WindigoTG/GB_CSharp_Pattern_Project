using UnityEngine;

namespace ShmupProject
{
    public interface IFireable
    {
        public void Fire(BullletConfig config, Vector3 position, Vector3 rotation, BulletManager bulletManager);
    }
}