using UnityEngine;

namespace Services
{
    public static class GlobalGradient
    {
        private static float _lerpSpeed;
        private static float _speed;
        private static float _progress;
        public static void Init(float speed,float lerpSpeed)
        {
            _speed = speed;
            _lerpSpeed = lerpSpeed;
        }
        public static float GetLerpSpeed() => _lerpSpeed;
        public static float GetGradientState()
        {
            _progress += _speed * Time.deltaTime;
            if (_progress > 1)
            {
                _progress = 0f;
            }
            return _progress;
        }
    }
}