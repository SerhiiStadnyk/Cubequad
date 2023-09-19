using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Runtime.SplinePoints
{
    public class SplinePoint : MonoBehaviour
    {
        [SerializeField]
        private SplinePoint _nextSplinePoint;

        [SerializeField]
        private float _rotationSpeed;

        protected List<ISplinePointComponent> splinePointComponents;

        public virtual SplinePoint NextSplinePoint => _nextSplinePoint;
        public virtual List<ISplinePointComponent> SplinePointComponents => splinePointComponents;
        public float RotationSpeed => _rotationSpeed;


        protected void Awake()
        {
            splinePointComponents = GetComponents<ISplinePointComponent>().ToList();
        }


        public virtual void PerformActions()
        {
            splinePointComponents.OfType<ISplinePointAction>().ToList().ForEach(splineAction => splineAction.PerformAction());
        }


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