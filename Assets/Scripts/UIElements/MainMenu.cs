using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UIElements
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIButton _startGameButton;

        public void Construct(Action startGame)
        {
            _startGameButton.Construct(startGame);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}