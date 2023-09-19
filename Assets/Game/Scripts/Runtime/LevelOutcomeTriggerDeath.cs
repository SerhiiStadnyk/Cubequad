using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class LevelOutcomeTriggerDeath : MonoBehaviour
    {
        [SerializeField]
        private LevelOutcomeHandler.LevelOutcome _outcomeType;

        private HealthComponent _healthComponent;
        private LevelOutcomeHandler _levelOutcomeHandler;


        [Inject]
        public void Inject(LevelOutcomeHandler levelOutcomeHandler)
        {
            _levelOutcomeHandler = levelOutcomeHandler;
        }


        protected void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }


        protected void OnEnable()
        {
            _healthComponent.HealthChanged += OnHealthChanged;
        }


        protected void OnDisable()
        {
            _healthComponent.HealthChanged -= OnHealthChanged;
        }


        private void OnHealthChanged()
        {
            if (_healthComponent.Health <= 0)
            {
                _levelOutcomeHandler.TriggerOutcome(_outcomeType);
            }
        }
    }
}
