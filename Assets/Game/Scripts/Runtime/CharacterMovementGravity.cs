using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class CharacterMovementGravity: MovementLogicBase
    {
        [SerializeField]
        private float _gravity = 10f;

        private CharacterMovementController _movementController;


        protected void Start()
        {
            _movementController = GetComponent<CharacterMovementController>();
        }


        public override Vector3 GetMovementVector()
        {
            return GravityMovement();
        }


        private Vector3 GravityMovement()
        {
            Vector3 result = Vector3.zero;
            if (!_movementController.IsJumping)
            {
                result = Vector3.down * (_gravity * Time.deltaTime);
            }

            return result;
        }
    }
}
