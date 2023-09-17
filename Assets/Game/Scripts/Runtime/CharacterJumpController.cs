using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Game.Scripts.Runtime
{
    public class CharacterJumpController : MovementLogicBase
    {
        [SerializeField]
        private float _jumpSpeed = 1.0f;

        private Coroutine _jumpCoroutine;
        private Vector3 _movementVector;


        public override Vector3 GetMovementVector()
        {
            Vector3 result = Vector3.zero;

            if (_jumpCoroutine != null)
            {
                result = _movementVector;
            }

            return result;
        }


        public void Jump(Vector3 startPoint, Vector3 endPoint, JumpData jumpData, Action callback)
        {
            if (_jumpCoroutine != null)
            {
                StopCoroutine(_jumpCoroutine);
            }

            _jumpCoroutine = StartCoroutine(JumpRoutine(startPoint, endPoint, jumpData, callback));
        }


        private IEnumerator JumpRoutine(Vector3 startPoint, Vector3 endPoint, JumpData jumpData, Action callback)
        {
            float elapsedTime = 0f;
            float tweenDuration = Vector3.Distance(startPoint, endPoint) / _jumpSpeed;
            float yDif = endPoint.y - startPoint.y;
            while (elapsedTime < tweenDuration)
            {
                elapsedTime += Time.deltaTime;

                // Calculate the new position along the path
                float t = Mathf.Clamp01(elapsedTime / tweenDuration); // Normalize time between 0 and 1
                Vector3 targetPos = CalculateTargetPos(startPoint, endPoint, jumpData, t, yDif);
                _movementVector = CalculateMovementVector(targetPos);

                yield return null;
            }

            callback?.Invoke();
            _movementVector = Vector3.zero;
            _jumpCoroutine = null;
        }


        private Vector3 CalculateTargetPos(Vector3 startPoint, Vector3 endPoint, JumpData jumpData, float t, float yDif)
        {
            //Calculate target position in motion frame
            Vector3 targetPos = Vector3.Lerp(startPoint, endPoint, t);
            targetPos = transform.InverseTransformPoint(targetPos);
            targetPos.x = 0;
            targetPos = transform.TransformPoint(targetPos);
            targetPos.y = CalculateHeight(startPoint, jumpData, yDif, t);
            return targetPos;
        }


        private Vector3 CalculateMovementVector(Vector3 targetPos)
        {
            Vector3 currentPosition = transform.position;
            Vector3 direction = targetPos - currentPosition;
            float distanceToMove = Vector3.Distance(targetPos, currentPosition);
            return direction.normalized * distanceToMove;
        }


        private static float CalculateHeight(Vector3 startPoint, JumpData jumpData, float yDif, float t)
        {
            // Calculate the new height (y) position using the jump curve
            float newY;
            if (Math.Abs(yDif) < 0.1f)
            {
                newY = startPoint.y + jumpData.YJumpCurve.Evaluate(t);
            }
            else
            {
                newY = startPoint.y + jumpData.YJumpCurve.Evaluate(t) * yDif;
            }

            return newY;
        }
    }
}
