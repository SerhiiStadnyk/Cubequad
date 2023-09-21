using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClipMap _audioClipMap;

        [SerializeField]
        private AudioSource _backgroundMusicSource;

        [SerializeField]
        private GameObject _sfxSourcePrefab;

        [SerializeField]
        private int _sfxPoolSize;

        [SerializeField]
        private Transform _sfxContainer;

        private List<AudioSource> _sfxPool;


        protected void Awake()
        {
            InitSfxPool();
        }


        public void PlayBackgroundClip(AudioClipId id, bool doReplay = false)
        {
            AudioClip chosenClip = _audioClipMap.GetClip(id);

            if (_backgroundMusicSource.clip != chosenClip || doReplay)
            {
                _backgroundMusicSource.clip = chosenClip;
                _backgroundMusicSource.Play();
            }
        }


        public void PlaySfx(AudioClipId id, Transform target)
        {
            AudioSource pooledSource = _sfxPool.FirstOrDefault(source => !source.gameObject.activeSelf);
            if (pooledSource == null)
            {
                pooledSource = ExpandSfxPool();
            }

            pooledSource.transform.position = target.position;
            pooledSource.clip = _audioClipMap.GetClip(id);
            pooledSource.gameObject.SetActive(true);
            pooledSource.Play();
            StartCoroutine(SfxRoutine(pooledSource));
        }


        private IEnumerator SfxRoutine(AudioSource sfxSource)
        {
            yield return new WaitWhile(() => sfxSource.isPlaying);
            sfxSource.gameObject.SetActive(false);
        }


        private AudioSource ExpandSfxPool()
        {
            GameObject sfxSourceObject = Instantiate(_sfxSourcePrefab, _sfxContainer.position, Quaternion.identity, _sfxContainer);
            sfxSourceObject.SetActive(false);
            AudioSource audioSource = sfxSourceObject.GetComponent<AudioSource>();
            _sfxPool.Add(audioSource);

            return audioSource;
        }


        private void InitSfxPool()
        {
            _sfxPool = new List<AudioSource>(_sfxPoolSize);
            for (int i = 0; i < _sfxPoolSize; i++)
            {
                ExpandSfxPool();
            }
        }
    }
}
