using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        private string _playerTag = "Player";

        [SerializeField]
        private AudioClipId _impactClipId;

        [SerializeField]
        private GameObject _destructionParticlesPrefab;

        private HealthComponent _healthComponent;
        private AudioManager _audioManager;


        [Inject]
        public void Inject(AudioManager audioManager)
        {
            _audioManager = audioManager;
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


        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                other.GetComponent<HealthComponent>().Health = -1;
                _healthComponent.Health = -1;

                _audioManager.PlaySfx(_impactClipId, transform);
            }
        }


        private void SpawnParticles()
        {
            GameObject particleObject = Instantiate(_destructionParticlesPrefab, transform.position, transform.rotation, transform.parent);
            ParticleSystem particle = particleObject.GetComponent<ParticleSystem>();
            ParticleSystem.ShapeModule shapeModule = particle.shape;
            shapeModule.scale = transform.localScale;
        }


        private void OnHealthChanged()
        {
            if (_healthComponent.Health <= 0)
            {
                SpawnParticles();
                Destroy(gameObject);
            }
        }
    }
}
