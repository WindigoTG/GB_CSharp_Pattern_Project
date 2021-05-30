using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ShmupProject
{
    public class Arc : Fireable
    {
        public override void Fire(BullletConfig config, Vector3 position, Vector3 rotation, BulletManager bulletManager)
        {
            config.Position = position;
            config.Rotation = rotation;

            if (config.LineCount == 1)
            {
                SubFire(config, position, rotation, bulletManager);
                return;
            }

            var start = config.Rotation.y * Mathf.PI / 180 - config.ArcAngle / 2;
            for (int i = 0; i < config.LineCount; i++)
            {
                var angle = start + i * (config.ArcAngle / (config.LineCount - 1));

                var currentConfig = config;
                currentConfig.Position = config.Position + (config.InitialRadius * new Vector3(Mathf.Sin(angle), 0.0f, Mathf.Cos(angle)));
                currentConfig.Rotation = (new Vector3(config.Rotation.x, angle * 180 / Mathf.PI, config.Rotation.z));

                SubFire(currentConfig, currentConfig.Position, currentConfig.Rotation, bulletManager);
            }
        }
    }
}