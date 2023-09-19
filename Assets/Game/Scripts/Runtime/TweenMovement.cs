using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class TweenMovement : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _targetOffset;

        [SerializeField]
        private float _speed;

        [SerializeField]
        private bool _looped;

        [SerializeField]
        private AnimationCurve _animationCurve;

        private Vector3 _originalPos;
        private LTDescr _tween;
        private bool _mirrored;
        private Transform _transform;


        protected void Awake()
        {
            _transform = transform;
            _originalPos = _transform.position;
        }


        protected void OnEnable()
        {
            StartTween();
        }


        private void StartTween()
        {
            Vector3 offset = _transform.right * _targetOffset.x + _transform.up * _targetOffset.y + _transform.forward * _targetOffset.z;
            Debug.LogWarning(offset);

            Vector3 targetPoint = _originalPos + offset;

            if (_mirrored)
            {
                targetPoint = _originalPos;
            }

            float time = Vector3.Distance(_transform.position, targetPoint) / _speed;
            _tween = LeanTween.move(gameObject, targetPoint, time).setEase(_animationCurve);

            if (_looped)
            {
                _tween.setOnComplete(OnComplete);
            }
        }


        private void OnComplete()
        {
            _mirrored = !_mirrored;
            StartTween();
        }


        protected void OnDisable()
        {
            LeanTween.cancel(_tween.id);
        }
    }
}
