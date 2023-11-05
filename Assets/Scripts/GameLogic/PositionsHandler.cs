using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic
{
    public class PositionsHandler:MonoBehaviour
    {
        public Transform CartSpawn;
        [SerializeField] private Transform _outPosition;
        [SerializeField] private Transform _trumpCartPos;
        [SerializeField] private Transform[] _playerPoses;
        private int _currentPlayerPlaceIndex;
        [SerializeField] public Transform OnTablePos;
        [SerializeField] private Vector3 _maxOnTableOffset;
        [SerializeField] private Vector3 _nextCartOnTableOffset;

        public Vector3 GetOutPos() => _outPosition.position;
        public Vector3 GetTrumpPos() => _trumpCartPos.position;
        
        public Transform GetNextPlayerViewPosition()
        {
            
            Transform nextPos = _playerPoses[_currentPlayerPlaceIndex];
            _currentPlayerPlaceIndex++;
            return nextPos;
        }

        public Vector3 GetMaxOnTableOffset() => _maxOnTableOffset;

        public Vector3 GetNextCartOnTableOffset() => _nextCartOnTableOffset;
    }
}