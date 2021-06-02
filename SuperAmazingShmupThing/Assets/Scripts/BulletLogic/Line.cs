using UnityEngine;

namespace ShmupProject
{
    public class Line : Fireable
    {
        public override void Fire(BullletConfig config, Vector3 position, Vector3 rotation)
        {
            for (var i = 0; i < config.BulletCount; i++)
            {
                config.BulletSpeed += config.DeltaSpeed;
                SubFire(config, position, rotation);
            }
        }
    }
}