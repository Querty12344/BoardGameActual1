using System;
using UnityEngine;

namespace UIElements
{
    public class GameHud : MonoBehaviour
    {
        [SerializeField] private UIButton _exitGameButton;
        [SerializeField] private UIButton _passButton;

        public void Construct(Action exitGame,Action pass)
        {
            _exitGameButton.Construct(exitGame);
            _passButton.Construct(pass);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}