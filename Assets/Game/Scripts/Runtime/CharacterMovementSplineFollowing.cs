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
        private Level _level;
        private SplinePoint _firstSpline;
        private LTDescr _rotationTween;


        [Inject]
        public void Inject(Level level)
        {
            _level = level;
        }


        private void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
            _firstSpline = _level.StartingPlatform.InputSplinePoints[0];
            _movementController.SetCurrentSplinePoint(_firstSpline);
        }


        protected void Update()
        {
            CalculateSplineReach();
        }


        public override Vector3 GetMovementVector()
        {
            return ForwardMovement();
        }


        private Vector3 ForwardMovement()
        {
            Vector3 result = Vector3.zero;
            if (!_movementController.IsJumping)
            {
                result = _movementController.CurrentSplinePoint.transform.forward * (_moveSpeed * Time.deltaTime);
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
            _movementController.SetCurrentSplinePoint(_movementController.CurrentSplinePoint.NextSplinePoint);
            RotateToSplinePoint();
            SplineActions();
        }


        private void SplineActions()
        {
            if (_movementController.CurrentSplinePoint.IsJumpingPoint)
            {
                _movementController.Jump(transform.position, _movementController.CurrentSplinePoint.NextSplinePoint.transform.position);
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
