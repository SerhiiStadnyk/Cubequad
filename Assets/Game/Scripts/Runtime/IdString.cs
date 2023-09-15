using UnityEngine;

namespace Game.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "IdString", menuName = "Game/IdString", order = 1)]
    public class StringId : ScriptableObject
    {
        [SerializeField]
        private string _id;

        public string Id => _id;
    }
}