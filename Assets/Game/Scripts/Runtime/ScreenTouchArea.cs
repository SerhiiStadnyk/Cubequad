using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Runtime
{
    public class ScreenTouchArea : MonoBehaviour
    {
        [SerializeField]
        private GameObject _transparentImage; // Reference to the transparent full-screen image

        public event Action<Vector3> OnTouch;


        private void Update()
        {
            // Check if there's a touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Check if the touch is not over the transparent image
                if (IsTouchOverImage(touch.position))
                {
                    // Handle swipe input here
                    Vector2 touchPosition = touch.position;
                    OnTouch?.Invoke(touchPosition);
                }
            }
        }


        bool IsTouchOverImage(Vector2 touchPosition)
        {
            // Check if the touch position is over the transparent image
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = touchPosition
            };

            // Raycast to check if the touch is over any UI elements (including transparent image)
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            // If the only result is the transparent image, it's not over UI
            return results.Count == 1 && results[0].gameObject == _transparentImage;
        }
    }
}