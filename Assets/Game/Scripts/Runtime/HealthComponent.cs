using System;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField]
        private int _maxHealth;

        [SerializeField]
        private int _currentHealth;

        public event Action HealthChanged;


        public int Health
        {
            get
            {
                return _currentHealth;
            }
            set
            {
                _currentHealth += value;
                if (_currentHealth > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }

                HealthChanged?.Invoke();
            }
        }


        protected void Start()
        {
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
    }
}
