using UnityEngine;

namespace Game.Scripts.Runtime.SplinePoints
{
    public class SplinePoint : MonoBehaviour
    {
        [SerializeField]
        private SplinePoint _nextSplinePoint;

        [SerializeField]
        private float _rotationSpeed;

        [SerializeField]
        private JumpData _jumpData;

        [SerializeField]
        protected bool _isJumpingPoint;

        public virtual SplinePoint NextSplinePoint => _nextSplinePoint;
        public float RotationSpeed => _rotationSpeed;
        public virtual bool IsJumpingPoint => _isJumpingPoint;
        public virtual JumpData JumpData => _jumpData;


        public void SetNextSplinePoint(SplinePoint nextPoint)
        {
            _nextSplinePoint = nextPoint;
        }


        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.15f);

            if (NextSplinePoint != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, NextSplinePoint.transform.position);
            }
        }
    }
}