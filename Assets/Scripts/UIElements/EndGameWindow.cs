using System;
using TMPro;
using UnityEngine;

namespace UIElements
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private UIButton _exitGameButton;
        [SerializeField] private UIButton _restartButton;
        [SerializeField] private GameObject _playerWinText;
        [SerializeField] private GameObject _playerLoseText;

        public void Construct(bool playerWon, Action restartGame, Action exitGame)
        {
            _playerWinText.SetActive(playerWon);
            _playerLoseText.SetActive(!playerWon);
            _exitGameButton.Construct(exitGame);
            _restartButton.Construct(restartGame);
            _restartButton.Construct(restartGame);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}