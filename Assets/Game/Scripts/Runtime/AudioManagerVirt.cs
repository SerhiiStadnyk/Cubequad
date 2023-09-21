using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime
{
    public class AudioManagerVirt : MonoBehaviour
    {
        private AudioManager _audioManager;


        [Inject]
        public void Inject(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }


        public void PlaySfx(AudioClipId clipId)
        {
            _audioManager.PlaySfx(clipId, transform);
        }


        public void PlayMusic(AudioClipId clipId)
        {
            _audioManager.PlayBackgroundClip(clipId);
        }
    }
}