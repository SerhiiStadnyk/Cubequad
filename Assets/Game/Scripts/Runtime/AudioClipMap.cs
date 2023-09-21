using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "AudioClip_Map", menuName = "Game/Create AudioClip Map", order = 1)]
    public class AudioClipMap : ScriptableObject
    {
        [SerializeField]
        private List<KeyValuePair> _map;


        public AudioClip GetClip(AudioClipId key)
        {
            return _map.FirstOrDefault(pair => pair.Key == key).Value;
        }


        [System.Serializable]
        private class KeyValuePair
        {
            [SerializeField]
            private AudioClipId _key;

            [SerializeField]
            private AudioClip _value;

            public AudioClipId Key => _key;

            public AudioClip Value => _value;
        }
    }
}
