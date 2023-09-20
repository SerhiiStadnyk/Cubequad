using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class ProgressionManager : MonoBehaviour
    {
        [SerializeField]
        private string _levelIdKey = "LevelId";

        [SerializeField]
        private LevelFactory _levelFactory;

        private int _currentLevelId;

        public int CurrentLevelId => _currentLevelId;


        protected void Awake()
        {
            LoadProgression();
        }


        public void ProgressFurther()
        {
            _currentLevelId += 1;

            if (_levelFactory.GetLevel(_currentLevelId) == null)
            {
                _currentLevelId = 0;
            }

            SaveProgression();
        }


        private void SaveProgression()
        {
            PlayerPrefs.SetInt(_levelIdKey, _currentLevelId);
            PlayerPrefs.Save();
        }


        private void LoadProgression()
        {
            _currentLevelId = PlayerPrefs.GetInt(_levelIdKey);
        }
    }
}