using Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField]
        private Level _level;

        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private CinemachineVirtualCamera _camera;

        private DiContainer _container;
        private GameObject _characterObject;


        [Inject]
        public void Inject(DiContainer container)
        {
            _container = container;
        }


        protected void Awake()
        {
            SpawnLevel();
            SpawnPlayer();
        }


        private void SpawnLevel()
        {
            _level.Init();
        }


        private void SpawnPlayer()
        {
            Transform startingPoint = _level.StartingPlatform.InputSplinePoints[0].transform;
            _character.transform.position = startingPoint.position;
            _character.GetComponent<CharacterMovementSplineFollowing>().Init(_container);
        }
    }
}