using System;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class LevelOutcomeHandler : MonoBehaviour
    {
        private bool _isFinished;

        public event Action<LevelOutcome> OnLevelOutcome;

        public enum LevelOutcome
        {
            Success,
            Fail
        }


        public void TriggerOutcome(LevelOutcome outcomeType)
        {
            _isFinished = true;
            if (!_isFinished)
            {
                OnLevelOutcome?.Invoke(outcomeType);
            }
        }
    }
}