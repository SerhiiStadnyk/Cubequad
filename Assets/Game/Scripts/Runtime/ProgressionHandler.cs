using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class ProgressionHandler : MonoBehaviour
    {
        private LevelOutcomeHandler _levelOutcomeHandler;
        private ProgressionManager _progressionManager;


        [Inject]
        public void Inject(LevelOutcomeHandler levelOutcomeHandler, ProgressionManager progressionManager)
        {
            _levelOutcomeHandler = levelOutcomeHandler;
            _progressionManager = progressionManager;
        }


        protected void OnEnable()
        {
            _levelOutcomeHandler.OnLevelOutcome += CheckOutcome;
        }


        protected void OnDisable()
        {
            _levelOutcomeHandler.OnLevelOutcome -= CheckOutcome;
        }


        private void CheckOutcome(LevelOutcomeHandler.LevelOutcome outcome)
        {
            if (outcome == LevelOutcomeHandler.LevelOutcome.Success)
            {
                _progressionManager.ProgressFurther();
            }
        }
    }
}
