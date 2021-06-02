using System;
using UnityEngine;

namespace ShmupProject
{
    [Serializable]
    public struct BullletConfig
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletAngularSpeed;
        [SerializeField] private float _lifeTime;

        [SerializeField] private float _fireDelay;

        [SerializeField] private int _bulletCount;
        [SerializeField] private float _deltaSpeed;

        [SerializeField] private int _lineCount;
        [SerializeField] private float _arcAngle;
        [SerializeField] private float _initialRadius;

        private Vector3 _position;
        private Vector3 _rotation;
        private float _angle;

        public BullletConfig(float bulletSpeed = 1.0f, float bulletAngularSpeed = 0.0f, float lifetime = 5.0f,
                            float fireDelay = 1.0f, int lineBulletCount = 1, float deltaSpeed = 1.0f,
                            int lineCount = 1, float arcAngle = 0.0f, float initialRadius = 0.0f)
        {
            _bulletSpeed = bulletSpeed;
            _bulletAngularSpeed = bulletAngularSpeed;
            _lifeTime = lifetime != 0 ? Math.Abs(lifetime) : 5.0f;
            _fireDelay = Math.Abs(fireDelay);
            _bulletCount = lineBulletCount > 0 ? lineBulletCount : 1;
            _deltaSpeed = deltaSpeed;
            _lineCount = lineCount > 0 ? lineCount : 1;
            _arcAngle = arcAngle;
            _initialRadius = initialRadius;
            _position = Vector3.zero;
            _rotation = Vector3.zero;
            _angle = 0.0f;
        }

        public float BulletSpeed
        {
            get => _bulletSpeed; 
            set => _bulletSpeed = value; 
        }

        public float BulletAngularSpeed
        {
            get => _bulletAngularSpeed * (float)Math.PI / 180; 
            set => _bulletAngularSpeed = value; 
        }

        public float LifeTime
        {
            get => _lifeTime; 
            set => _lifeTime = value; 
        }

        public float FireDelay
        {
            get => _fireDelay; 
            set => _fireDelay = Math.Abs(value); 
        }

        public int BulletCount
        {
            get => _bulletCount; 
            set => _bulletCount = value > 0 ? value : 1; 
        }

        public float DeltaSpeed
        {
            get => _deltaSpeed;
            set => _deltaSpeed = value;
        }

        public int LineCount
        { 
            get => _lineCount;
            set => _lineCount = value > 0 ? value : 1;
        }

        public float ArcAngle
        {
            get => _arcAngle * (float)Math.PI / 180;
            set => _arcAngle = value;
        }

        public float InitialRadius
        {
            get => _initialRadius;
            set => _initialRadius = value;
        }

        public Vector3 Position
        {
            get => _position; 
            set => _position = value; 
        }

        public Vector3 Rotation
        {
            get => _rotation; 
            set => _rotation = value; 
        }

        public float Angle
        {
            get => _angle; 
            set => _angle = value; 
        }
    }
}