using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class CurrentLevelPresenter : MonoBehaviour
    {
        [SerializeField]
        private string _format = "Level {0}";

        private TMP_Text _text;
        private ProgressionManager _progressionManager;


        [Inject]
        public void Inject(ProgressionManager progressionManager)
        {
            _progressionManager = progressionManager;
        }


        void Start()
        {
            _text = GetComponent<TMP_Text>();
            string levelNumber = (_progressionManager.CurrentLevelId + 1).ToString();
            _text.text = string.Format(_format, levelNumber);
        }
    }
}
