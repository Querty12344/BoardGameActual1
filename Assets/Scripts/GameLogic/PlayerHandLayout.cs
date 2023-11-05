using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;

namespace GameLogic
{
    public class PlayerHandLayout : MonoBehaviour
    {
        private List<CartMover> _carts;
        private float _defaultCartOffset;
        private float _yCardRotationCof;
        private float _chosenCartOffset;
        private float _xCartRotationCof;
        private float _yOffset;
        private CartMover _chosenCart;
        public void Construct(PerformanceSettings settings,IInputService inputService)
        {
            _xCartRotationCof  =settings.XRotCof;
            _yCardRotationCof = settings.YRotCof;
            _defaultCartOffset = settings.CartOffset;
            _chosenCartOffset = settings.ChoseCartOffset;
            _yOffset = settings.YOffset;
            _carts = new List<CartMover>();
            inputService.OnMouseEnterCart += ChoseCart;
        }

        public void AddCart(Cart cart)
        {
            _carts.Add(cart.Mover);
            cart.Mover.transform.SetParent(transform);
            RecalculateLayout();
        }

        public void RemoveCart(Cart cart)
        {
            Debug.Log("Remove It");
            _carts.Remove(cart.Mover);
            cart.Mover.transform.SetParent(null);
            cart.Mover.SetDefaultRotation();
            RecalculateLayout();
        }

        private void ChoseCart(Cart cart)
        {
            if (cart == null && _chosenCart != null)
            {
                _chosenCart = null;
                RecalculateLayout();
                return;
            }
            if (cart == null)
            {
                return;
            }

            if (_carts.Contains(cart.Mover) && _chosenCart != cart.Mover)
            {
                _chosenCart = cart.Mover;
                RecalculateLayout();
            }
        }
        private void RecalculateLayout()
        {
            float cartOffset = _defaultCartOffset /Mathf.Sqrt(_carts.Count);
            bool foundedUppest = false;
            for (int i = 0; i < _carts.Count; i++)
            {
                Vector3 nextPos = Vector3.right * cartOffset * i + transform.position +
                                  Vector3.left * cartOffset * _carts.Count * 0.5f;
                float fromCenterOffsetCof = (i+1 - _carts.Count/2f)*cartOffset;
                _carts[i].gameObject.GetComponentInChildren<Canvas>().sortingOrder = 1000 - Mathf.Abs((int)((i + 1)*2 - _carts.Count));
                nextPos -= Vector3.up*Mathf.Abs(fromCenterOffsetCof)*_yOffset;
                float yRotation = fromCenterOffsetCof * _yCardRotationCof;
                float xRotation = _xCartRotationCof;
                _carts[i].SetRotation(Quaternion.Euler(xRotation,yRotation,0f));
                if (_chosenCart != null)
                {
                    if ( i == _carts.IndexOf(_chosenCart))
                    {
                        _carts[i].SetPosition(nextPos + Vector3.up*_chosenCartOffset);
                    }else
                    {
                        int cof2 = (_carts.IndexOf(_chosenCart) - i) > 0 ? 1:-1;
                        float cof = Mathf.Sqrt(Mathf.Abs(_carts.IndexOf(_chosenCart) - i))*cof2;
                        _carts[i].SetPosition(nextPos + cof*Vector3.left*_chosenCartOffset);
                    }
                }
                else
                {
                    _carts[i].SetPosition(nextPos);
                }
            }
        }
    }
}