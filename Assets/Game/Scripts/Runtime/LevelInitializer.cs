using Game.Scripts.Runtime.SplinePoints;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField]
        private Level _level;

        [SerializeField]
        private GameObject _player;

        private DiContainer _container;


        [Inject]
        public void Inject(DiContainer container)
        {
            _container = container;
        }


        protected void Start()
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
            _container.InstantiatePrefab(_player, startingPoint.position, transform.rotation, transform.parent);
        }
    }
}