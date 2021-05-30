using UnityEngine;

namespace ShmupProject
{
    internal sealed class ObjectPoolManager
    {
        private static ObjectPoolManager _poolManager;

        private ObjectPool _playerBulletsPool;
        private ObjectPool _enemyBulletsPool;
        private ObjectPool _enemiesPool;

        private Transform _playerBulletParent;
        private Transform _enemyBulletParent;

        private ObjectPoolManager()
        {
            _playerBulletParent = new GameObject(MagicStrings.Player_Bullets_Parent_Object).transform;
            _enemyBulletParent = new GameObject(MagicStrings.Enemy_Bullets_Parent_Object).transform;
        }

        public static ObjectPoolManager GetInstance()
        {
            if (_poolManager == null)
                _poolManager = new ObjectPoolManager();
            return _poolManager;
        }

        public ObjectPool PlayerBulletsPool
        {
            get
            {
                if (_playerBulletsPool == null)
                    _playerBulletsPool = new ObjectPool(Resources.Load<GameObject>(MagicStrings.Player_Bullet_Prefab), _playerBulletParent);
                return _playerBulletsPool;
            }
        }

        public ObjectPool EnemyBulletsPool
        {
            get
            {
                if (_enemyBulletsPool == null) 
                    _enemyBulletsPool = new ObjectPool(Resources.Load<GameObject>(MagicStrings.Enemy_Bullet_Prefab), _enemyBulletParent);
                return _enemyBulletsPool;
            }
        }
    }
}