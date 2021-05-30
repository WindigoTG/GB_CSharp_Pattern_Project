using UnityEngine;

namespace ShmupProject
{
    public class SingleBullet : Fireable
    {
        public override void Fire(BullletConfig config, Vector3 position, Vector3 rotation, BulletManager bulletManager)
        {
            Transform bullet = ObjectPoolManager.GetInstance().EnemyBulletsPool.Pop().transform;
            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);
            BulletMoveCommand command = new BulletMoveCommand(bullet, config.BulletSpeed, config.BulletAngularSpeed, config.LifeTime, bulletManager);
            bulletManager.AddCommand(command);
        }
    }
}