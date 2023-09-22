#if UNITY_EDITOR
using UnityEngine;

namespace Game.Scripts.Runtime
{
    [ExecuteInEditMode]
    public partial class SafeAreaAdjuster
    {
        protected void Update()
        {
            UpdateSafeArea();
        }
    }
}
#endif
