using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    [CreateAssetMenu(fileName = "LevelFactory", menuName = "Game/Create Level Factory", order = 1)]
    public class LevelFactory : ScriptableObject
    {
        [SerializeField]
        private List<LevelIdMap> _levelIdPairs;


        public GameObject GetLevel(int id)
        {
            GameObject result = null;
            LevelIdMap levelMap = _levelIdPairs.FirstOrDefault(pair => pair.LevelId == id);
            if (levelMap != null)
            {
                result = levelMap.LevelPrefab;
            }

            return result;
        }


        [System.Serializable]
        private class LevelIdMap
        {
            [SerializeField]
            private GameObject _levelPrefab;

            [SerializeField]
            private int _levelId;

            public GameObject LevelPrefab => _levelPrefab;

            public int LevelId => _levelId;
        }
    }
}
