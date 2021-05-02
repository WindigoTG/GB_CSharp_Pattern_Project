using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public class EnemyPool
    {
        private readonly Stack<Enemy> _stack = new Stack<Enemy>();
        private IEnemyFactory _enemyFactory;
        private Transform _parentObject;

        public EnemyPool(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _parentObject = new GameObject(MagicStrings.Enemies_Parent_Object).transform;
        }

        public void Push(Enemy enemy)
        {
            _stack.Push(enemy);
            enemy.gameObject.SetActive(false);
        }

        public Enemy Pop()
        {
            Enemy enemy;
            if (_stack.Count == 0)
            {
                enemy = _enemyFactory.CreateEnemy();
                enemy.gameObject.transform.parent = _parentObject;
            }
            else
            {
                enemy = _stack.Pop();
            }
            enemy.gameObject.SetActive(true);

            return enemy;
        }
    }
}