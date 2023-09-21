using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField]
        private ProgressionManager _progressionManager;

        [SerializeField]
        private AudioManager _audioManager;


        public override void InstallBindings()
        {
            Container.BindInstance(_progressionManager).AsSingle();
            Container.BindInstance(_audioManager).AsSingle();
        }
    }
}
