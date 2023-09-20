using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField]
        private ProgressionManager _progressionManager;


        public override void InstallBindings()
        {
            Container.BindInstance(_progressionManager).AsSingle();
        }
    }
}
