using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class CameraShakeOnImpact : MonoBehaviour
    {
        [SerializeField]
        private float _shakeDuration = 0.4f;

        [SerializeField]
        private float _shakeStrength = 2f;

        [SerializeField]
        private CinemachineVirtualCamera _camera;

        [SerializeField]
        private HealthComponent _targetHealth;

        private CinemachineBasicMultiChannelPerlin _channelPerlin;
        private Coroutine _shakeRoutine;


        protected void Awake()
        {
            _channelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }


        protected void OnEnable()
        {
            _targetHealth.HealthChanged += Shake;
        }


        protected void OnDisable()
        {
            _targetHealth.HealthChanged -= Shake;
        }


        private void Shake()
        {
            if (_shakeRoutine != null)
            {
                StopCoroutine(_shakeRoutine);
                _shakeRoutine = null;
            }

            _shakeRoutine = StartCoroutine(ShakeRoutine());
        }


        private IEnumerator ShakeRoutine()
        {
            float timer = 0;
            _channelPerlin.m_AmplitudeGain = _shakeStrength;
            while (timer < _shakeDuration)
            {
                timer += Time.deltaTime;
                float t = timer / _shakeDuration;
                float intensity = Mathf.Lerp(_shakeStrength, 0, t);
                _channelPerlin.m_AmplitudeGain = intensity;

                yield return null;
            }
            _channelPerlin.m_AmplitudeGain = 0;
        }
    }
}