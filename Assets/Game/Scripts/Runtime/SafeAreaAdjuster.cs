using System;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public partial class SafeAreaAdjuster : MonoBehaviour
    {
        [SerializeField]
        private float _updateTolerance = 1f;

        private RectTransform _rectTransform;
        private Vector2 _deltaPos;

        protected void Awake()
        {
            UpdateCanvas();
        }


        private void UpdateCanvas()
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            Rect safeArea = Screen.safeArea;
            // Convert safe area coordinates to canvas space
            Vector2 min = safeArea.min;
            Vector2 max = safeArea.max;

            // Adjust the anchors of the safe area panel
            _rectTransform.anchorMin = new Vector2(min.x / Screen.width, min.y / Screen.height);
            _rectTransform.anchorMax = new Vector2(max.x / Screen.width, max.y / Screen.height);
        }
    }
}