using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Runtime.SplinePoints;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Game.Scripts.Runtime
{
    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField]
        private CharacterController _characterController;

        [SerializeField]
        private CharacterJumpController _characterJumpController;

        private List<MovementLogicBase> _movementLogic;
        private bool _isJumping;
        private SplinePoint _currentSplinePoint;

        public bool IsJumping => _isJumping;
        public CharacterController CharacterController => _characterController;

        public SplinePoint CurrentSplinePoint => _currentSplinePoint;


        protected void Start()
        {
            _movementLogic = GetComponents<MovementLogicBase>().ToList();
        }


        private void Update()
        {
            Vector3 moveDirection = Vector3.zero;

            foreach (MovementLogicBase movement in _movementLogic)
            {
                moveDirection += movement.GetMovementVector();
            }

            _characterController.Move(moveDirection);
            DumbCameraMovement();
        }


        private void DumbCameraMovement()
        {
            // var foo = _currentSplinePoint.transform.InverseTransformVector(Camera.main.transform.position);
            // foo.x = 0;
            // Camera.main.transform.position = _currentSplinePoint.transform.TransformVector(foo);
        }


        public void SetCurrentSplinePoint(SplinePoint newPoint)
        {
            _currentSplinePoint = newPoint;
        }


        public void Jump(Vector3 startPoint, Vector3 endPoint, JumpData jumpData)
        {
            _isJumping = true;
            _characterJumpController.Jump(startPoint, endPoint, jumpData, () => _isJumping = false);
        }
    }
}