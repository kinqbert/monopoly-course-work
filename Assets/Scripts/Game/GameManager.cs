using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        private Board.Board _gameBoard;

        private List<GameParticipant> _players;
        private int _currentPlayerIndex;
        private GameParticipant _currentPlayer;

        private bool _isDiceRolled;
        private DiceRoller _diceRoller;

        private bool _isGameOver;
        private bool _isMoving;

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

            _diceRoller = FindObjectOfType<DiceRoller>();

            _isGameOver = false;
            
            rollButton = GameObject.Find("Roll-Dice-Button").GetComponent<Button>(); // finding the button
            rollButton.onClick.AddListener(RollDice); // RollDice will be called when the button is clicked
        }

        void Update()
        {
            // RollDice will be called when the space key is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RollDice();
            }
        }

        public void RollDice()
        {
            _diceRoller.RollTheDice();
        }
    }
}
