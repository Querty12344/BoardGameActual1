using UnityEngine;

namespace GameLogic
{
    public class PlayerView:MonoBehaviour
    {
        [SerializeField] private Transform _handTransform;
        [SerializeField] private GameObject _attackMarker;
        [SerializeField] private GameObject _defuseMarker;
        [SerializeField] private GameObject _sleepMarker;
        
        public Transform GetHandTransform() => _handTransform;

        public void SetActiveMarker(Marker notActive)
        {
            DeactivateMarkers();
            switch (notActive)
            {
                case Marker.Attack:
                    _attackMarker.gameObject.SetActive(true);
                    return;
                case Marker.Defuse:
                    _defuseMarker.gameObject.SetActive(true);
                    return;
                case Marker.NotActive:
                    _sleepMarker.gameObject.SetActive(true);
                    return;
            }
        }

        private void DeactivateMarkers()
        {
            _attackMarker.gameObject.SetActive(false);
            _defuseMarker.gameObject.SetActive(false);
            _sleepMarker.gameObject.SetActive(false);
        }
    }
}