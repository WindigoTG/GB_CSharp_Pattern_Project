namespace ShmupProject
{
    public class EnemyFactory : IEnemyFactory
    {
        private IWeaponEnemy _weapon;

        public EnemyFactory()
        {
            _weapon = ServiceLocator.GetService<WeaponFactory>().CreateWeapon(EnemyWeaponType.SingleShotStraight, true);
        }

        public EnemyFactory(IWeaponEnemy weapon)
        {
            _weapon = weapon;
        }

        public Enemy CreateEnemy()
        {
            var enemy = new Enemy();
            var weapon = (_weapon as EnemyWeapon).Clone();
            enemy.SetWeapon((IWeaponEnemy)weapon);
            return enemy;
        }

        public void SetWeapon(IWeaponEnemy weapon)
        {
            _weapon = weapon;
        }
    }
}