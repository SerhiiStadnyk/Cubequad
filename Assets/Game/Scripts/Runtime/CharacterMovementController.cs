using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
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

        [SerializeField]
        private string _idleAnimTrigger = "Idle";

        [SerializeField]
        private string _danceAnimTrigger = "Dance";

        private List<MovementLogicBase> _movementLogic;
        private bool _isJumping;
        private bool _isStopped;

        private LevelOutcomeHandler _levelOutcomeHandler;

        public bool IsJumping => _isJumping;
        public CharacterController CharacterController => _characterController;


        [Inject]
        public void Inject(LevelOutcomeHandler levelOutcomeHandler)
        {
            _levelOutcomeHandler = levelOutcomeHandler;
        }


        protected void Awake()
        {
            _movementLogic = GetComponents<MovementLogicBase>().ToList();
            _animatorController.SetTrigger(_runAnimTrigger);
        }


        protected void OnEnable()
        {
            _levelOutcomeHandler.OnLevelOutcome += StopMovement;
        }


        protected void OnDisable()
        {
            _levelOutcomeHandler.OnLevelOutcome -= StopMovement;
        }


        private void StopMovement(LevelOutcomeHandler.LevelOutcome levelOutcome)
        {
            Debug.LogWarning("StopMovement");
            _isStopped = true;
            switch (levelOutcome)
            {
                case LevelOutcomeHandler.LevelOutcome.Success:
                    _animatorController.SetTrigger(_danceAnimTrigger);
                    break;
                default:
                    _animatorController.SetTrigger(_idleAnimTrigger);
                    break;
            }
        }


        private void Update()
        {
            if (!_isStopped)
            {
                Vector3 moveDirection = Vector3.zero;

                foreach (MovementLogicBase movement in _movementLogic)
                {
                    moveDirection += movement.GetMovementVector();
                }

                _characterController.Move(moveDirection);
            }
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