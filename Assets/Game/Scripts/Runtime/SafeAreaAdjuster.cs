using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Runtime
{
    public partial class SafeAreaAdjuster : MonoBehaviour
    {
        private Rect _safeArea;
        private RectTransform _rectTransform;
        private CanvasScaler _canvasScaler;


        protected void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasScaler = GetComponentInParent<CanvasScaler>();
        }


        protected void OnEnable()
        {
            Canvas.willRenderCanvases += UpdateSafeArea;
        }


        protected void OnDisable()
        {
            Canvas.willRenderCanvases -= UpdateSafeArea;
        }


        private void UpdateSafeArea()
        {
            Rect safeArea = Screen.safeArea;
            if (safeArea != _safeArea)
            {
                _safeArea = safeArea;

                float bottomPixels = Screen.safeArea.y;
                float topPixel = Screen.currentResolution.height - (Screen.safeArea.y + Screen.safeArea.height);

                float bottomRatio = bottomPixels / Screen.currentResolution.height;
                float topRatio = topPixel / Screen.currentResolution.height;

                Vector2 referenceResolution = _canvasScaler.referenceResolution;
                float bottomUnits = referenceResolution.y * bottomRatio;
                float topUnits = referenceResolution.y * topRatio;

                _rectTransform.offsetMin = new Vector2(_rectTransform.offsetMin.x, bottomUnits);
                _rectTransform.offsetMax = new Vector2(_rectTransform.offsetMax.x, -topUnits);
            }
        }
    }
}