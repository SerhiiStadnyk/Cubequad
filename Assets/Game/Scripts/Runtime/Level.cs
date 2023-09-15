using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private PlatformSegment _startingPlatform;

        private DiContainer _container;

        public PlatformSegment StartingPlatform => _startingPlatform;


        [Inject]
        public void Inject(DiContainer container)
        {
            _container = container;
        }


        public void Init()
        {
            _container.BindInstance(this).AsSingle();
        }
    }
}