using UnityEngine;

namespace ShmupProject
{
    public class LeadTargeting
    {
        public bool IsPredicting { get; set; }

        private Vector3 _previousPosition;
        private Vector3 _currentPosition;
        private Vector3 _predictedPosition;

        private Vector3 _originPosition;

        public Vector3 OriginPosition
        {
            set { _originPosition = value; }
        }

        public Vector3 TargetPosition
        {
            get
            {
                return _predictedPosition;
            }

            set
            {
                if (_previousPosition != null)
                    _previousPosition = _currentPosition;
                else
                    _previousPosition = value;

                _currentPosition = value;

                if (_currentPosition != _previousPosition)
                {
                    float distance = 1.0f;
                    if (_originPosition != null)
                        distance = (_currentPosition - _originPosition).magnitude;

                    _predictedPosition = _currentPosition + (_currentPosition - _previousPosition).normalized * distance / 2.5f;
                }
                else
                    _predictedPosition = _currentPosition;
            }
        }
    }
}