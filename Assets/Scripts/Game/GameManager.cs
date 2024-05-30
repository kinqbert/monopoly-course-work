using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private Board.Board _gameBoard;

        private List<GameParticipant> _players;
        private int _currentPlayerIndex;
        private GameParticipant _currentPlayer;

        private Dice _dice;

        private bool _isDiceRolled;
        private bool _isGameOver;

        public Button rollButton;

        void Start()
        {
            // initializing players
            _players = new List<GameParticipant>
            {
                new GameParticipant("Player 1"),
                new GameParticipant("Player 2")
            };

            _currentPlayerIndex = 0;
            _currentPlayer = _players[_currentPlayerIndex];

            _dice = GameObject.Find("DiceManager").GetComponent<Dice>();

            _isGameOver = false;
            _isDiceRolled = false;
            
            rollButton = GameObject.Find("Roll-Dice-Button").GetComponent<Button>(); // finding the button
            rollButton.onClick.AddListener(RollDice); // RollDice will be called when the button is clicked
        }

        void Update()
        {
            if (_isGameOver)
            {
                // handle game over state
                return;
            }
            
            // RollDice will be called when the space key is pressed
            if (Input.GetKeyDown(KeyCode.Space) && !_isDiceRolled)
            {
                RollDice();
            }
            
            if (_isDiceRolled)
            {
                _currentPlayer.Move(_dice.GetTotal());
            }
        }

        public void RollDice()
        {
            _dice.RollTheDice();
        }

        // Call this method after the player completes their turn
        private void EndTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            _currentPlayer = _players[_currentPlayerIndex];
            _isDiceRolled = false; // Reset the flag for the next player
        }
    }
}
