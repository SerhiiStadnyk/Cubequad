using System;
using Game.Scripts.Runtime.SplinePoints;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class SplinePathTracker : MonoBehaviour
    {
        private SplinePoint _splinePointTarget;
        private SplinePoint _splinePointOrigin;
        private bool _canTrack;

        public SplinePoint SplinePointTarget => _splinePointTarget;
        public SplinePoint SplinePointOrigin => _splinePointOrigin;

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
            _splinePointTarget = newPoint;
            if (newPoint != null)
            {
                _canTrack = true;
            }
        }


        private void CalculateSplineReach()
        {
            Vector3 targetPointPosition = _splinePointTarget.transform.position;
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
            _splinePointOrigin = _splinePointTarget;
            SetCurrentSplinePoint(_splinePointTarget.NextSplinePoint);
            if (_splinePointTarget == null)
            {
                _canTrack = false;
            }

            OnSplineReached?.Invoke();
        }
    }
}
