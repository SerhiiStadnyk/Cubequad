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

        [SerializeField]
        private Transform _splineFollower;

        private CharacterMovementController _movementController;
        private Level _level;
        private SplinePoint _firstSpline;
        private LTDescr _rotationTween;
        private bool _hasTarget;


        public void Init(DiContainer container)
        {
            _level = container.Resolve<Level>();

            _movementController = GetComponent<CharacterMovementController>();
            _firstSpline = _level.StartingPlatform.InputSplinePoints[0];
            _movementController.SetCurrentSplinePoint(_firstSpline);
            _hasTarget = true;
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
                result = _movementController.CurrentSplinePoint.transform.forward * (_moveSpeed * Time.deltaTime);
                CalculateSplineReach();
            }
            if (_hasTarget)
            {
                Vector3 foo = _movementController.CurrentSplinePoint.transform.InverseTransformPoint(transform.position);
                foo.x = 0;
                foo = _movementController.CurrentSplinePoint.transform.TransformPoint(foo);
                _splineFollower.position = foo;
                _splineFollower.rotation = transform.rotation;
            }

            return result;
        }


        private void CalculateSplineReach()
        {
            Vector3 targetPointPosition = _movementController.CurrentSplinePoint.transform.position;
            Vector3 direction = targetPointPosition - transform.position;
            direction.y = 0f;

            float distanceAlongDirection = Vector3.Dot(direction, transform.forward);
            if (distanceAlongDirection <= 0f)
            {
                OnSplineReached();
            }
        }


        private void OnSplineReached()
        {
            if (_movementController.CurrentSplinePoint.NextSplinePoint == null)
            {
                _hasTarget = false;
                _movementController.SetCurrentSplinePoint(null);
            }
            else
            {
                SplineActions();
                _movementController.SetCurrentSplinePoint(_movementController.CurrentSplinePoint.NextSplinePoint);
                RotateToSplinePoint();
            }
        }


        private void SplineActions()
        {
            if (_movementController.CurrentSplinePoint.IsJumpingPoint)
            {
                _movementController.Jump(
                    transform.position,
                    _movementController.CurrentSplinePoint.NextSplinePoint.transform.position,
                    _movementController.CurrentSplinePoint.JumpData);
            }
        }


        private void RotateToSplinePoint()
        {
            if (_rotationTween != null)
            {
                LeanTween.cancel(_rotationTween.id);
            }

            float rotSpeed = (_movementController.CurrentSplinePoint.RotationSpeed > 0) ? _movementController.CurrentSplinePoint.RotationSpeed : _rotationSpeed;

            Quaternion targetRotation = _movementController.CurrentSplinePoint.transform.rotation;
            Quaternion currentRot = transform.rotation;
            float rotDuration = Quaternion.Angle(targetRotation, currentRot) / rotSpeed;

            _rotationTween = LeanTween.rotate(gameObject, targetRotation.eulerAngles, rotDuration)
                .setEase(LeanTweenType.linear);
        }
    }
}
