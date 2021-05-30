using UnityEngine;

namespace ShmupProject
{
    public class EnemyFactory : IEnemyFactory
    {
        private IWeaponEnemy _weapon;

        public EnemyFactory()
        {
            _weapon = new SingleShotStraightEnemy();
        }

        public EnemyFactory(IWeaponEnemy weapon)
        {
            _weapon = weapon;
        }

        public Enemy CreateEnemy()
        {
            var enemy = (GameObject)GameObject.Instantiate(Resources.Load(MagicStrings.Enemy_Prefab), new Vector3(0, 1, 10), Quaternion.Euler(0, 180, 0));
            enemy.GetComponent<Enemy>().SetWeapon(_weapon);
            return enemy.GetComponent<Enemy>();
        }

        public void SetWeapon(IWeaponEnemy weapon)
        {
            _weapon = weapon;
        }
    }
}