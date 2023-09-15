using System.Collections.Generic;
using Game.Scripts.Runtime.SplinePoints;
using UnityEngine;

namespace Game.Scripts.Runtime
{
    public class PlatformSegment : MonoBehaviour
    {
        [SerializeField]
        private List<SplinePoint> _inputSplinePoints;

        [SerializeField]
        private List<PlatformSegment> _outputPlatforms;

        public IReadOnlyList<PlatformSegment> OutputPlatforms => _outputPlatforms;

        public IReadOnlyList<SplinePoint> InputSplinePoints => _inputSplinePoints;
    }
}