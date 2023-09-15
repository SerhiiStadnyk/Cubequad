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


        public override void InstallBindings()
        {
            Container.BindInstance(_character).WithConcreteId(_playerCharacterId.Id);
        }
    }
}
