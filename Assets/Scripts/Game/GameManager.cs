using System.Collections.Generic;
using UI;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private List<GameParticipant> _players;
        private int _currentPlayerIndex;
        private GameParticipant _currentPlayer;

        private bool _isDiceRolled;
        private bool _isGameOver;

        public Dice dice;
        public Board.Board gameBoard;
        public Button rollButton;
        public Button endTurnButton;
        public GameObject playerPrefab;

        void Start()
        {
            rollButton.onClick.AddListener(RollDice); // RollDice will be called when the button is clicked
            endTurnButton.onClick.AddListener(EndTurn); // EndTurn will be called when the button is clicked
            
            // Initializing players
            _players = new List<GameParticipant>
            {
                InstantiatePlayer("Player 1"),
                InstantiatePlayer("Player 2"),
                InstantiatePlayer("Player 3"),
                InstantiatePlayer("Player 4")
            };

            _currentPlayerIndex = 0;
            _currentPlayer = _players[_currentPlayerIndex];

            dice.OnDiceRollComplete = OnDiceRollComplete; // Subscribe to the dice roll complete event

            _isGameOver = false;
            _isDiceRolled = false;

            // Initially disable the end turn button
            endTurnButton.interactable = false;

            // Update player info UI
            UpdatePlayerInfoUI();
        }

        void Update()
        {
            if (!_isGameOver)
            {
                // Block/Unblock Roll button based on whether the dice is rolled
                if (_isDiceRolled)
                {
                    GameUI.BlockRollButton();
                    GameUI.UnblockEndTurnButton();
                }
                else
                {
                    GameUI.BlockEndTurnButton();
                    GameUI.UnblockRollButton();
                }

                // RollDice will be called when the space key is pressed
                if (Input.GetKeyDown(KeyCode.Space) && !_isDiceRolled)
                {
                    RollDice();
                }

                // Block all interactions when the confirmation window is active
                if (ConfirmationWindow.IsActive)
                {
                    GameUI.BlockAll();
                }
                else
                {
                    GameUI.UnblockAll();
                }
            }
        }

        private GameParticipant InstantiatePlayer(string playerName)
        {
            GameObject playerObj = Instantiate(playerPrefab); // Instantiate the prefab
            GameParticipant player = playerObj.GetComponent<GameParticipant>();
            player.Initialize(playerName);
            return player;
        }

        public void RollDice()
        {
            if (!_isDiceRolled)
            {
                dice.RollTheDice();
                _isDiceRolled = true;
                endTurnButton.interactable = true; // Enable end turn button after rolling the dice
            }
        }

        private void OnDiceRollComplete()
        {
            _currentPlayer.Move(dice.GetTotal());
            _currentPlayer.CurrentTile.Field.OnPlayerLanded(_currentPlayer);
            UpdatePlayerInfoUI(); // Update player info UI after moving
        }

        private void EndTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            _currentPlayer = _players[_currentPlayerIndex];
            _isDiceRolled = false; // Reset the dice rolled flag for the next player
            endTurnButton.interactable = false; // Disable end turn button until the next roll
            UpdatePlayerInfoUI(); // Update player info UI for the new player
        }

        private void UpdatePlayerInfoUI()
        {
            GameUI.SetPlayerInfo(_currentPlayer);
        }
    }
}
