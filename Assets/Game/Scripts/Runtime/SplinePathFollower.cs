using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class SplinePathFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform _targetTransform;

        [SerializeField]
        private SplinePathTracker _splinePathTracker;

        private Transform _transform;


        protected void Awake()
        {
            _transform = transform;
        }


        public void Init()
        {
            Follow();
        }


        protected void Update()
        {
            if (_splinePathTracker != null && _splinePathTracker.SplinePointTarget != null)
            {
                Follow();
            }
        }


        private void Follow()
        {
            Vector3 followerPos = _splinePathTracker.SplinePointTarget.transform.InverseTransformPoint(_targetTransform.position);
            followerPos.x = 0;
            followerPos = _splinePathTracker.SplinePointTarget.transform.TransformPoint(followerPos);
            _transform.position = followerPos;
            _transform.rotation = _targetTransform.rotation;
        }
    }
}
