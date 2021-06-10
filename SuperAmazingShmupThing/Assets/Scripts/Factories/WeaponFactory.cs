namespace ShmupProject
{
    public class WeaponFactory
    {
        public IWeaponEnemy CreateWeapon(EnemyWeaponType type, bool tracking)
        {
            IFireable bullet = GetBullet(type);
            BullletConfig config = GetConfig(type);

            if (tracking)
                return new EnemyWeaponTracking(config, bullet);
            else
                return new EnemyWeaponNonTracking(config, bullet);
        }

        private IFireable GetBullet(EnemyWeaponType type)
        {
            switch (type)
            {
                case EnemyWeaponType.LineStraight:
                    return new SingleBullet(BulletOwner.Enemy).FiredInLine();

                case EnemyWeaponType.ThreeLinesSpreadgun:
                case EnemyWeaponType.TenLinesSpinning:
                    return new SingleBullet(BulletOwner.Enemy).FiredInLine().FiredInArc();

                case EnemyWeaponType.SingleShotStraight:
                default:
                    return new SingleBullet(BulletOwner.Enemy);
            }
        }

        private BullletConfig GetConfig(EnemyWeaponType type)
        {
            float bulletSpeed = 1.0f;
            float angularSpeed = 0.0f;
            float fireDelay = 1.0f;
            float bulletLifeTime = 5.0f;
            int bulletCount = 1;
            float deltaSpeed = 1.0f;
            int lineCount = 1;
            float arcAngle = 0.0f;
            float initialRadius = 0.0f;

            switch (type)
            {
                case EnemyWeaponType.LineStraight:
                    {
                        bulletSpeed = 2.0f;
                        fireDelay = 2.0f;
                        bulletCount = 5;
                        break;
                    }

                case EnemyWeaponType.ThreeLinesSpreadgun:
                    {
                        fireDelay = 3.5f;
                        bulletCount = 5;
                        lineCount = 3;
                        arcAngle = 30;
                        initialRadius = 0.2f;
                        break;
                    }

                case EnemyWeaponType.TenLinesSpinning:
                    {
                        angularSpeed = -50.0f;
                        fireDelay = 10f;
                        bulletLifeTime = 7.1f;
                        bulletCount = 5;
                        lineCount = 10;
                        arcAngle = 324;
                        initialRadius = 0.5f;
                        break;
                    }

                case EnemyWeaponType.SingleShotStraight:
                default:
                    {
                        bulletSpeed = 5.0f;
                        fireDelay = 2.0f;
                        break;
                    }
            }

            return new BullletConfig(bulletSpeed, angularSpeed, bulletLifeTime, fireDelay, bulletCount, deltaSpeed, lineCount, arcAngle, initialRadius);
        }
    }

    public enum EnemyWeaponType
    {
        SingleShotStraight,
        LineStraight,
        TenLinesSpinning,
        ThreeLinesSpreadgun
    }
}