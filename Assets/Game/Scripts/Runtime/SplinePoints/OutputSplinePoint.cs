using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Runtime.SplinePoints
{
    public class OutputSplinePoint : SplinePoint
    {
        [SerializeField]
        private List<OutputData> _outputData;

        [SerializeField]
        private StringId _playerCharacterId;

        [SerializeField]
        private PlatformSegment _platformSegment;

        private GameObject _character;

        public override SplinePoint NextSplinePoint => GetNextSplinePoint();


        [Inject]
        public void Inject(DiContainer container)
        {
            //_character = container.ResolveId<GameObject>(_playerCharacterId.Id);
            _character = container.Resolve<GameObject>();
        }


        private SplinePoint GetNextSplinePoint()
        {
            Transform characterPos;

            if (_character != null)
            {
                characterPos = _character.transform;
            }
            else
            {
                characterPos = transform;
            }

            OutputData chosenOutput = _outputData
                .Where(output => output.OutputPoint != null)
                .OrderBy(output => Vector3.Distance(characterPos.position, output.OutputPoint.transform.position))
                .FirstOrDefault();

            SplinePoint nextSplinePoint = null;
            if (_platformSegment != null && _platformSegment.OutputPlatforms != null && _platformSegment.OutputPlatforms.Count > 0)
            {
                PlatformSegment nextPlatform = _platformSegment.OutputPlatforms[chosenOutput.NextPlatformId];
                if (nextPlatform != null)
                {
                    nextSplinePoint = nextPlatform.InputSplinePoints[chosenOutput.NextPlatformInputId];
                }
            }

            return nextSplinePoint;
        }


        [System.Serializable]
        private struct OutputData
        {
            [SerializeField]
            private SplinePoint _outputPoint;

            [SerializeField]
            private int _nextPlatformId;

            [SerializeField]
            private int _nextPlatformInputId;

            public SplinePoint OutputPoint => _outputPoint;

            public int NextPlatformId => _nextPlatformId;

            public int NextPlatformInputId => _nextPlatformInputId;
        }
    }
}