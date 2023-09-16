using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class CharacterJumpController : MonoBehaviour
    {
        [SerializeField]
        private float _jumpSpeed = 1.0f;

        private Coroutine _jumpCoroutine;


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

                // Calculate the new positions for x, y, and z using the curves
                float t = Mathf.Clamp01(elapsedTime / tweenDuration); // Normalize time between 0 and 1
                float newX = Mathf.Lerp(startPoint.x, endPoint.x, t);
                float newZ = Mathf.Lerp(startPoint.z, endPoint.z, t);
                float newY;
                if (Math.Abs(yDif) < 0.1f)
                {
                    newY = startPoint.y + jumpData.YJumpCurve.Evaluate(t);
                }
                else
                {
                    newY = startPoint.y + jumpData.YJumpCurve.Evaluate(t) * yDif;
                }

                // Update the GameObject's position
                transform.position = new Vector3(newX, newY, newZ);

                yield return null;
            }

            callback?.Invoke();
            _jumpCoroutine = null;
        }
    }
}
