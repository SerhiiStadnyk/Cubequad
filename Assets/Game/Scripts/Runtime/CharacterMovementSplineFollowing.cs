using Game.Scripts.Runtime.SplinePoints;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class CharacterMovementSplineFollowing : MovementLogicBase
    {
        [SerializeField]
        private float _moveSpeed = 5f;

        [SerializeField]
        private float _rotationSpeed;

        private CharacterMovementController _movementController;
        private SplinePathTracker _splinePathTracker;
        private Level _level;
        private SplinePoint _firstSpline;
        private LTDescr _rotationTween;
        private bool _hasTarget;


        protected void OnDisable()
        {
            if (_splinePathTracker != null)
            {
                _splinePathTracker.OnSplineReached -= OnSplinePointReached;
            }
        }


        public void Init(DiContainer container)
        {
            _level = container.Resolve<Level>();

            _movementController = GetComponent<CharacterMovementController>();
            _splinePathTracker = GetComponent<SplinePathTracker>();
            _firstSpline = _level.StartingPlatform.InputSplinePoints[0];
            _splinePathTracker.SetCurrentSplinePoint(_firstSpline);
            _hasTarget = true;

            _splinePathTracker.OnSplineReached += OnSplinePointReached;
        }


        public override Vector3 GetMovementVector()
        {
            return ForwardMovement();
        }


        private Vector3 ForwardMovement()
        {
            Vector3 result = Vector3.zero;
            if (_hasTarget && !_movementController.IsJumping)
            {
                result = _splinePathTracker.SplinePointTarget.transform.forward * (_moveSpeed * Time.deltaTime);
            }

            return result;
        }


        private void RotateToSplinePoint()
        {
            if (_rotationTween != null)
            {
                LeanTween.cancel(_rotationTween.id);
            }

            float rotSpeed = (_splinePathTracker.SplinePointTarget.RotationSpeed > 0) ? _splinePathTracker.SplinePointTarget.RotationSpeed : _rotationSpeed;

            Quaternion targetRotation = _splinePathTracker.SplinePointTarget.transform.rotation;
            Quaternion currentRot = transform.rotation;
            float rotDuration = Quaternion.Angle(targetRotation, currentRot) / rotSpeed;

            _rotationTween = LeanTween.rotate(gameObject, targetRotation.eulerAngles, rotDuration)
                .setEase(LeanTweenType.linear);
        }


        private void OnSplinePointReached()
        {
            if (_splinePathTracker.SplinePointTarget == null)
            {
                _hasTarget = false;
            }
            else
            {
                RotateToSplinePoint();
            }
        }
    }
}
