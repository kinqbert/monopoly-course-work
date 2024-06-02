using System.Collections.Generic;
using UI;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private List<GameParticipant> _players;
        private int _currentPlayerIndex;
        private GameParticipant _currentPlayer;

        private bool _isDiceRolled;
        private bool _isGameOver;

        public Dice dice;
        public Button rollButton;
        public Button endTurnButton;
        public GameObject playerPrefab;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            rollButton.onClick.AddListener(RollDice); // RollDice will be called when the button is clicked
            endTurnButton.onClick.AddListener(EndTurn); // EndTurn will be called when the button is clicked

            // initializing players
            _players = new List<GameParticipant>
            {
                InstantiatePlayer("Player 1"),
                InstantiatePlayer("Player 2"),
                InstantiatePlayer("Player 3"),
                InstantiatePlayer("Player 4")
            };

            _currentPlayerIndex = 0;
            _currentPlayer = _players[_currentPlayerIndex];

            dice.OnDiceRollComplete = OnDiceRollComplete; // subscribe to the dice roll complete event

            _isGameOver = false;
            _isDiceRolled = false;

            // initially disabling the end turn button
            GameUI.BlockEndTurnButton();

            // updating player info UI
            GameUI.SetPlayerInfo(_currentPlayer);
        }

        private void Update()
        {
            if (!_isGameOver)
            {
                // block/unblock Roll button based on whether the dice is rolled
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

                // rollDice will be called when the space key is pressed
                if (Input.GetKeyDown(KeyCode.Space) && !_isDiceRolled)
                {
                    RollDice();
                }

                // blocking all interactions when the confirmation window is active
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
            GameObject playerObj = Instantiate(playerPrefab); // instantiating the player piece prefab
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
                GameUI.BlockRollButton();
            }
        }

        private void OnDiceRollComplete()
        {
            if (_currentPlayer.IsInJail)
            {
                if (dice.IsDouble())
                {
                    _currentPlayer.ReleaseFromJail();
                    GameUI.ShowNotification($"{_currentPlayer.Name} rolled a double and was released from jail and can move.");
                    _currentPlayer.Move(dice.GetTotal()); // allowing the player to move if they roll a double on the same turn
                    _currentPlayer.CurrentTile.Field.OnPlayerLanded(_currentPlayer);
                }
                else
                {
                    _currentPlayer.DecrementJailTurns();
                    if (_currentPlayer.JailTurns == 0)
                    {
                        GameUI.ShowNotification($"{_currentPlayer.Name} will be released from jail on next turn.");
                    }
                    else
                    {
                        GameUI.ShowNotification($"{_currentPlayer.Name} is still in jail for {_currentPlayer.JailTurns} more turn(s).");
                    }
                }
            }
            else
            {
                _currentPlayer.Move(dice.GetTotal());
                _currentPlayer.CurrentTile.Field.OnPlayerLanded(_currentPlayer);
            }
            GameUI.UpdatePlayerInfo(); // updating player info UI after moving
        }

        private void EndTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            _currentPlayer = _players[_currentPlayerIndex];
            _isDiceRolled = false; // reset dice rolled flag for the next player
            GameUI.BlockEndTurnButton();
            GameUI.SetPlayerInfo(_currentPlayer); // updating player info UI for the new player
        }

        public GameParticipant GetCurrentPlayer()
        {
            return _currentPlayer;
        }
    }
}
