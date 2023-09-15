using UnityEngine;

namespace Game.Scripts.Runtime
{
    public abstract class MovementLogicBase: MonoBehaviour
    {
        public abstract Vector3 GetMovementVector();
    }
}
