using System;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class CharacterJumpController : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve _curve;

        [SerializeField]
        private float _jumpSpeed = 1.0f;


        public void Jump(Vector3 startPoint, Vector3 endPoint, Action callback)
        {
            float jumpDuration = Vector3.Distance(startPoint, endPoint) / _jumpSpeed;

            LTDescr jumpTweenY = LeanTween.moveY(gameObject, endPoint.y, jumpDuration)
                .setEase(_curve)
                .setOnComplete(() =>
                {
                    callback?.Invoke();
                });

            LTDescr jumpTweenZ = LeanTween.moveZ(gameObject, endPoint.z, jumpDuration)
                .setEase(LeanTweenType.linear);
        }
    }
}
