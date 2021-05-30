using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    internal sealed class ObjectPool
    {
        private readonly Stack<GameObject> _stack = new Stack<GameObject>();
        private readonly GameObject _prefab;
        private Transform _parentObject;

        public ObjectPool(GameObject prefab, Transform parentObject)
        {
            _prefab = prefab;
            _parentObject = parentObject;
        }

        public void Push(GameObject gameObject)
        {
            _stack.Push(gameObject);
            gameObject.SetActive(false);
        }

        public GameObject Pop()
        {
            GameObject gameObject;
            if (_stack.Count == 0)
            {
                gameObject = Object.Instantiate(_prefab);
                gameObject.transform.parent = _parentObject;
            }
            else
            {
                gameObject = _stack.Pop();
            }
            gameObject.SetActive(true);

            return gameObject;
        }
    }
}