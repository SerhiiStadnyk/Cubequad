using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class CharacterMovementStrafe : MovementLogicBase
    {
        [SerializeField]
        private float _strafeSpeed = 10f;

        [SerializeField]
        private float _xMoveConstrainsDefault = 1.5f;

        private Camera _camera;
        private CharacterMovementController _movementController;


        protected void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
            _camera = Camera.main;
        }


        public override Vector3 GetMovementVector()
        {
            return StrafeMovement();
        }


        private Vector3 StrafeMovement()
        {
            Vector3 result = Vector3.zero;

            if (Input.touchCount > 0)
            {
                Vector3 touchPosition = Input.GetTouch(0).position;
                Vector3 charPos = transform.position;

                float camDist = Vector3.Distance(_camera.transform.position, charPos);
                Vector3 targetPos = _camera.ScreenToWorldPoint(new Vector3(touchPosition.x, charPos.y, camDist));

                // Adjust targetPos to be on the same y-plane and z-plane as charPos
                Vector3 localTargetPos = transform.InverseTransformPoint(targetPos);
                localTargetPos.y = 0;
                localTargetPos.z = 0;
                targetPos = transform.TransformPoint(localTargetPos);

                Vector3 offset = targetPos - charPos;
                float magnitude = offset.magnitude;

                if (magnitude > 0.1f && CanStrafe())
                {
                    result = offset.normalized * (_strafeSpeed * Time.deltaTime);
                }
            }

            return result;
        }


        private bool CanStrafe()
        {
            return CanMaintainConstrains();
        }


        private bool IsInConstrains()
        {
            // Set constraints for strafing
            float constrains = _movementController.CurrentSplinePoint.StrafeConstrains > 0
                ? _movementController.CurrentSplinePoint.StrafeConstrains
                : _xMoveConstrainsDefault;

            // Calculate the character's position relative to the spline point
            Vector3 characterToSplinePoint = _movementController.CurrentSplinePoint.transform.InverseTransformPoint(transform.position);

            return Mathf.Abs(characterToSplinePoint.x) <= constrains;
        }


        private bool CanMaintainConstrains()
        {
            // Determine strafe direction based on touch position
            Vector3 strafeSide = Input.GetTouch(0).position.x < Screen.width / 2f ? Vector3.left : Vector3.right;

            // Set constraints for strafing
            float constrains = _movementController.CurrentSplinePoint.StrafeConstrains > 0
                ? _movementController.CurrentSplinePoint.StrafeConstrains
                : _xMoveConstrainsDefault;

            // Calculate the character's position relative to the spline point
            Vector3 characterToSplinePoint = _movementController.CurrentSplinePoint.transform.InverseTransformPoint(transform.position);

            // Calculate the normalized strafe destination
            float normalizeStrafeDest = characterToSplinePoint.x + (strafeSide.x * _strafeSpeed * Time.deltaTime);

            // Determine if strafing is allowed based on constraints and direction
            return Mathf.Abs(normalizeStrafeDest) <= constrains;
        }


        private bool IsSolidSurfaceAvailable(Vector3 pivotPoint, Vector3 targetPoint)
        {
            bool result = false;

            float rayLength = _movementController.CharacterController.height * 0.6f;
            Vector3 side = targetPoint.x >= pivotPoint.x ? Vector3.right : Vector3.left;
            Vector3 rayOrigin = pivotPoint + side * _movementController.CharacterController.radius;

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayLength))
            {
                result = true;
            }

            return result;
        }
    }
}
