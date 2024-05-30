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
        public GameObject playerPrefab; // Assign this in the Inspector with the GameParticipant component

        void Start()
        {
            _dice = GameObject.Find("DiceManager").GetComponent<Dice>();
            _gameBoard = GameObject.Find("Board").GetComponent<Board.Board>();
            rollButton = GameObject.Find("Roll-Dice-Button").GetComponent<Button>(); // finding the button
            rollButton.onClick.AddListener(RollDice); // RollDice will be called when the button is clicked
            
            // initializing players
            _players = new List<GameParticipant>
            {
                InstantiatePlayer("Player 1"),
                InstantiatePlayer("Player 2")
            };

            _currentPlayerIndex = 0;
            _currentPlayer = _players[_currentPlayerIndex];

            _dice.OnDiceRollComplete = OnDiceRollComplete; // Subscribe to the dice roll complete event

            _isGameOver = false;
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
            _dice.RollTheDice();
        }

        private void OnDiceRollComplete()
        {
            // Debug.Log("Current player: " + _currentPlayer.Name);
            _currentPlayer.Move(_dice.GetTotal());
            EndTurn();
        }

        // Call this method after the player completes their turn
        private void EndTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            _currentPlayer = _players[_currentPlayerIndex];
        }
    }
}
