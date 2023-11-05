using UnityEngine;
using UnityEngine.Serialization;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Create PerformanceSettings", fileName = "PerformanceSettings", order = 0)]
    public class PerformanceSettings:ScriptableObject
    {
        public float CartMovingSpeed;
        public float CartOffset;
        public float YRotCof;
        public float XRotCof;
        public float YOffset;
        public float GradientSpeed;
        public float GradientLerpSpeed;
        public float CartRotSpeed;
        public float ChoseCartOffset;
        public Vector3 CartDragOffset;
        public int BotWaitingTime;
    }
}