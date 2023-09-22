using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class CharacterMovementStrafe : MovementLogicBase
    {
        [SerializeField]
        private float _strafeSpeed = 10f;

        [SerializeField]
        private float _raycastRadiusOffset;

        [SerializeField]
        private float _raycastHeightOffset;

        [SerializeField]
        private ScreenTouchArea _screenTouchArea;

        private Camera _camera;
        private CharacterMovementController _movementController;
        private SplinePathTracker _splinePathTracker;
        private Vector3 _strafeValue;


        protected void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
            _splinePathTracker = GetComponent<SplinePathTracker>();
            _camera = Camera.main;
        }


        protected void OnEnable()
        {
            _screenTouchArea.OnTouch += OnScreenTouch;
        }


        protected void OnDisable()
        {
            _screenTouchArea.OnTouch -= OnScreenTouch;
        }


        public override Vector3 GetMovementVector()
        {
            Vector3 returnValue = _strafeValue;
            _strafeValue = Vector3.zero;
            return returnValue;
        }


        private void OnScreenTouch(Vector3 touchPos)
        {
            _strafeValue = Vector3.zero;
            Vector3 charPos = transform.position;

            float camDist = Vector3.Distance(_camera.transform.position, charPos);
            Vector3 targetPos = _camera.ScreenToWorldPoint(new Vector3(touchPos.x, charPos.y, camDist));

            // Adjust targetPos to be on the same y-plane and z-plane as charPos
            Vector3 localTargetPos = transform.InverseTransformPoint(targetPos);
            localTargetPos.y = 0;
            localTargetPos.z = 0;
            targetPos = transform.TransformPoint(localTargetPos);

            Vector3 offset = targetPos - charPos;
            float magnitude = offset.magnitude;

            if (magnitude > 0.1f && CanStrafe(targetPos, targetPos))
            {
                _strafeValue = offset.normalized * (_strafeSpeed * Time.deltaTime);
            }
        }


        private bool CanStrafe(Vector3 pivotPoint, Vector3 targetPoint)
        {
            return _splinePathTracker.SplinePointTarget != null && CanMaintainConstrains(pivotPoint, targetPoint);
        }


        private bool CanMaintainConstrains(Vector3 pivotPoint, Vector3 targetPoint)
        {
            // Determine strafe direction based on touch position
            Vector3 strafeSide = Input.GetTouch(0).position.x < Screen.width / 2f ? Vector3.left : Vector3.right;

            return _movementController.IsJumping ? IsSolidSurfaceAvailable(_splinePathTracker.SplinePointTarget.transform, strafeSide) : IsSolidSurfaceAvailable(transform, strafeSide);
        }


        private bool IsSolidSurfaceAvailable(Transform pivotTransform, Vector3 strafeSide)
        {
            bool result = false;

            float rayLength = _movementController.CharacterController.height * 0.5f + _raycastHeightOffset;
            Vector3 offset = pivotTransform.right * (strafeSide.x * (_movementController.CharacterController.radius + _raycastRadiusOffset));
            Vector3 pivotPosition = pivotTransform.InverseTransformPoint(transform.position);
            pivotPosition.z = 0;
            pivotPosition.y = 0;
            pivotPosition = pivotTransform.TransformPoint(pivotPosition);
            Vector3 rayOrigin = pivotPosition + offset;

            if (Physics.Raycast(rayOrigin, pivotTransform.up * -1, out RaycastHit hit, rayLength))
            {
                result = true;
            }

            return result;
        }
    }
}
