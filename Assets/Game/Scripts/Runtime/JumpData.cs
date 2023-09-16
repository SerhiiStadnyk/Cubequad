using UnityEngine;

namespace Game.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "JumpData_", menuName = "Game/Create Jump Data", order = 1)]
    public class JumpData : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve _yJumpCurve;

        public AnimationCurve YJumpCurve => _yJumpCurve;
    }
}