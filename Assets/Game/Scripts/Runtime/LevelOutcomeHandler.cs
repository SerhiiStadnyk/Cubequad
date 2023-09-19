using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Runtime
{
    public class LevelOutcomeHandler : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _onSuccessEvents;

        [SerializeField]
        private UnityEvent _onFailEvents;

        private bool _isFinished;

        public event Action<LevelOutcome> OnLevelOutcome;

        public enum LevelOutcome
        {
            Success,
            Fail
        }


        public void TriggerOutcome(LevelOutcome outcomeType)
        {
            if (!_isFinished)
            {
                OnLevelOutcome?.Invoke(outcomeType);

                switch (outcomeType)
                {
                    case LevelOutcome.Success:
                        _onSuccessEvents?.Invoke();
                        break;
                    case LevelOutcome.Fail:
                        _onFailEvents?.Invoke();
                        break;
                }
            }
            _isFinished = true;
        }
    }
}