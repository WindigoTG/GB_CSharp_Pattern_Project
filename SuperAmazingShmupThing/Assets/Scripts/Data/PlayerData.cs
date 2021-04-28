using UnityEngine;

namespace ShmupProject
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField, Range(0, 100)] private float _speed;
        [SerializeField] private Vector3 _position;
        [SerializeField] private float _boundX;
        [SerializeField] private float _boundZ;
        public float Speed => _speed;
        public Vector3 Position => _position;
        public float Xbound => _boundX;
        public float Zbound => _boundZ;
    }
}