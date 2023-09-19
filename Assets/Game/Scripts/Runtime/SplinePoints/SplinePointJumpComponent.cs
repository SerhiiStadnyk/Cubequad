using UnityEngine;

namespace Game.Scripts.Runtime.SplinePoints
{
    public class SplinePointJumpComponent : MonoBehaviour, ISplinePointComponent
    {
        [SerializeField]
        private JumpData _jumpData;

        public JumpData JumpData => _jumpData;
    }
}