using Game.Scripts.Runtime.SplinePoints;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class LevelOutcomeTriggerSplinePointReached : MonoBehaviour, ISplinePointAction
    {
        [SerializeField]
        private LevelOutcomeHandler.LevelOutcome _outcomeType;

        private LevelOutcomeHandler _levelOutcomeHandler;


        [Inject]
        public void Inject(LevelOutcomeHandler levelOutcomeHandler)
        {
            _levelOutcomeHandler = levelOutcomeHandler;
        }


        void ISplinePointAction.PerformAction()
        {
            _levelOutcomeHandler.TriggerOutcome(_outcomeType);
        }
    }
}
