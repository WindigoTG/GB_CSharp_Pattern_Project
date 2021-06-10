using UnityEngine;

namespace ShmupProject
{
    public class SingleBullet : Fireable
    {
        private BulletOwner _owner;

        public SingleBullet(BulletOwner owner)
        {
            _owner = owner;
        }

        public override void Fire(BullletConfig config, Vector3 position, Vector3 rotation)
        {
            Transform bullet = _owner == BulletOwner.Enemy ? 
                                ServiceLocator.GetService<BulletPoolManager>().EnemyBulletsPool.Pop().transform :
                                ServiceLocator.GetService<BulletPoolManager>().PlayerBulletsPool.Pop().transform;

            bullet.position = position;
            bullet.rotation = Quaternion.Euler(rotation);

            IBulletMoveCommand command;
            if (_owner == BulletOwner.Enemy)
                command = new EnemyBulletMoveCommand(bullet, config.BulletSpeed, config.BulletAngularSpeed, config.LifeTime);
            else
                command = new PlayerBulletMoveCommand(bullet, config.BulletSpeed, config.LifeTime);

            ServiceLocator.GetService<BulletManager>().AddCommand(command);
        }
    }

    public enum BulletOwner
    {
        Player,
        Enemy
    }
}