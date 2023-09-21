using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class ParticlesLifetimeHandler : MonoBehaviour
    {
        private ParticleSystem _particleSystem;


        protected void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }


        protected void Update()
        {
            if (!_particleSystem.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}