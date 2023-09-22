using System.Collections;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private LevelFactory _levelFactory;

        [SerializeField]
        private Transform _levelContainer;

        [SerializeField]
        private CinemachineVirtualCamera _cameraVirt;

        [SerializeField]
        private SplinePathFollower _splinePathFollower;

        private GameObject _characterObject;
        private Level _currentLevel;

        private DiContainer _container;
        private ProgressionManager _progressionManager;


        [Inject]
        public void Inject(DiContainer container, ProgressionManager progressionManager)
        {
            _container = container;
            _progressionManager = progressionManager;
        }


        protected void Start()
        {
            LoadLevel();
            InitPlayer();
        }


        public void Init()
        {
            _character.GetComponent<CharacterMovementController>().MovementStatus(true);
        }


        private void LoadLevel()
        {
            int levelId = _progressionManager.CurrentLevelId;
            GameObject levelPrefab = _levelFactory.GetLevel(levelId);
            _currentLevel = _container.InstantiatePrefab(levelPrefab, Vector3.zero, Quaternion.identity, _levelContainer).GetComponent<Level>();
            _currentLevel.Init(_container);
        }


        private void InitPlayer()
        {
            _cameraVirt.enabled = false;
            Transform startingPoint = _currentLevel.StartingPlatform.InputSplinePoints[0].transform;
            _character.transform.position = startingPoint.position;

            _character.GetComponent<CharacterMovementSplineFollowing>().Init(_container);
            _splinePathFollower.Init();
            _cameraVirt.enabled = true;
        }
    }
}