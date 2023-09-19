using System;
using Game.Scripts.Runtime.SplinePoints;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class SplinePathTracker : MonoBehaviour
    {
        private SplinePoint _currentSplinePoint;
        private bool _canTrack = false;

        public SplinePoint CurrentSplinePoint => _currentSplinePoint;

        public event Action OnSplineReached;


        protected void Update()
        {
            if (_canTrack)
            {
                CalculateSplineReach();
            }
        }


        public void SetCurrentSplinePoint(SplinePoint newPoint)
        {
            _currentSplinePoint = newPoint;
            if (newPoint != null)
            {
                _canTrack = true;
            }
        }


        private void CalculateSplineReach()
        {
            Vector3 targetPointPosition = _currentSplinePoint.transform.position;
            Vector3 direction = targetPointPosition - transform.position;
            direction.y = 0f;

            float distanceAlongDirection = Vector3.Dot(direction, transform.forward);
            if (distanceAlongDirection <= 0f)
            {
                SplineReached();
            }
        }


        private void SplineReached()
        {
            OnSplineReached?.Invoke();
            SetCurrentSplinePoint(_currentSplinePoint.NextSplinePoint);
            if (_currentSplinePoint == null)
            {
                _canTrack = false;
            }
        }
    }
}
