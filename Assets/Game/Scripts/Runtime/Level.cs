using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private PlatformSegment _startingPlatform;

        public PlatformSegment StartingPlatform => _startingPlatform;


        public void Init(DiContainer container)
        {
            container.BindInstance(this).AsSingle();
        }
    }
}