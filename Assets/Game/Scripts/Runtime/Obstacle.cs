using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        private string _playerTag = "Player";

        private HealthComponent _healthComponent;


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


        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                other.GetComponent<HealthComponent>().Health = -1;
                _healthComponent.Health = -1;
            }
        }


        private void OnHealthChanged()
        {
            if (_healthComponent.Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
