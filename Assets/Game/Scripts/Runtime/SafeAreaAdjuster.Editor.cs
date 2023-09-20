#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    [ExecuteInEditMode]
    public partial class SafeAreaAdjuster
    {
        private void OnEnable()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        private void OnDisable()
        {
            EditorApplication.update -= OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            UpdateCanvas();
        }
    }
}
#endif
