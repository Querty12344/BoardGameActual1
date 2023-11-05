using System;
using Unity.VisualScripting;
using UnityEngine;

namespace UIElements
{
    public class UIButton:MonoBehaviour
    {
        private Action _action;

        public void Construct(Action action)
        {
            _action = action;
        }

        public void Click()
        {
            _action.Invoke();
        }
    }
}