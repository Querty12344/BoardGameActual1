

using System;
using UnityEngine;

namespace GameLogic
{
    public class CartMover:MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private Vector3 _dragStartPosition;
        private Quaternion _dragStartRotation;
        private Transform _startParent;
        private float _movingSpeed;
        private float _rotationSpeed;
        private Vector3 _startScale;
        private ICartDragger _cartDragger;


        public void Construct(float movingSpeed,float rotationSpeed,ICartDragger cartDragger)
        {
            _cartDragger = cartDragger;
            _rotationSpeed = rotationSpeed;
            _movingSpeed = movingSpeed;
            cartDragger.OnDraggingStarted += DisableCollider;
            cartDragger.OnDraggingEnded += EnableCollider;
        }

        private void Awake()
        {
            _startScale = transform.localScale;
        }

        public void StartDrag()
        {
            _startParent = transform.parent;
            transform.SetParent(null);
            _dragStartPosition = _targetPosition;
            _dragStartRotation = _targetRotation;
            transform.localScale = _startScale;
        }

        public void StopDrag()
        {
            transform.SetParent(_startParent);
            _targetPosition = _dragStartPosition;
            _targetRotation = _dragStartRotation;
            transform.localScale = _startScale;
        }

        public void SetPosition(Vector3 position)
        {
            transform.localScale = _startScale;
            _targetPosition = position;
        }

        public void SetRotation(Quaternion nextRotation)
        {
            _targetRotation = nextRotation;
        }

        public void Remove()
        {
            _cartDragger.OnDraggingStarted -= DisableCollider;
            _cartDragger.OnDraggingEnded -= EnableCollider;
        }
        public void SetDefaultRotation()
        {
            _targetRotation = Quaternion.Euler(0, 0, 0);
        }
        
        private void EnableCollider(){
            if (_collider == null)
            {
                return;
            }
            _collider.enabled = true;   
        }

        private void DisableCollider()
        {
            if (_collider == null)
            {
                return;
            }
            _collider.enabled = false;   
        }
        private void FixedUpdate()
        {
            if (_targetPosition != Vector3.zero)
            { 
                transform.position  = Vector3.Lerp(transform.position,_targetPosition,_movingSpeed);
            }
            if(transform.rotation == _targetRotation) return;
            transform.rotation = _targetRotation;
        }
        
    }
}