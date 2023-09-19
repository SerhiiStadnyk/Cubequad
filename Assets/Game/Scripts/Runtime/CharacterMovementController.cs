using System.Collections.Generic;
using System.Linq;
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

        [SerializeField]
        private Animator _animatorController;

        [SerializeField]
        private string _runAnimTrigger = "Run";

        [SerializeField]
        private string _jumpAnimTrigger = "Jump";

        [SerializeField]
        private string _landAnimTrigger = "Land";

        private List<MovementLogicBase> _movementLogic;
        private bool _isJumping;

        public bool IsJumping => _isJumping;
        public CharacterController CharacterController => _characterController;


        protected void Start()
        {
            _movementLogic = GetComponents<MovementLogicBase>().ToList();
            _animatorController.SetTrigger(_runAnimTrigger);
        }


        private void Update()
        {
            Vector3 moveDirection = Vector3.zero;

            foreach (MovementLogicBase movement in _movementLogic)
            {
                moveDirection += movement.GetMovementVector();
            }

            _characterController.Move(moveDirection);
        }


        public void Jump(Vector3 startPoint, Vector3 endPoint, JumpData jumpData)
        {
            _animatorController.SetTrigger(_jumpAnimTrigger);
            _isJumping = true;
            _characterJumpController.Jump(startPoint, endPoint, jumpData, OnJumpEnd);
        }


        private void OnJumpEnd()
        {
            _animatorController.SetTrigger(_landAnimTrigger);
            _isJumping = false;
        }
    }
}