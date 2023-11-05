using System;
using System.Collections;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLogic
{
    public class InputService:IInputService
    {
        public event Action<Cart> MouseStartDragCart;
        public event Action<Cart> OnMouseEnterCart;
        public event Action MouseStopDragCart;
        private ICoroutineRunner _coroutineRunner;
        
        public InputService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Init()
        {
            _coroutineRunner.StartCoroutine(ObserveInput());
        }
        public Vector3 GetMousePosition()
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    return hitInfo.point;
                }
            }
            return new Vector3(0f, 0f, 0f);
        }

        private IEnumerator ObserveInput()
        {
            while (true)
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                        if (hitInfo.collider.gameObject.TryGetComponent<Cart>(out var cart))
                        {
                            MouseStartDragCart?.Invoke(cart);
                        }
                    }
                }
                else
                {
                    MouseStopDragCart?.Invoke();
                }
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out RaycastHit hitInfo2))
                {
                    if (hitInfo2.collider.gameObject.TryGetComponent<Cart>(out var cart))
                    {
                         OnMouseEnterCart?.Invoke(cart);
                    }
                    else
                    {
                        OnMouseEnterCart?.Invoke(null);
                    }
                }
                yield return null;
            }
        }
        

        
    }
}