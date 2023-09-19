using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private StringId _playerCharacterId;

        [SerializeField]
        private LevelOutcomeHandler _levelOutcomeHandler;


        public override void InstallBindings()
        {
            Container.BindInstance(_character).WithConcreteId(_playerCharacterId.Id);
            Container.BindInstance(_levelOutcomeHandler).AsSingle();
        }
    }
}
