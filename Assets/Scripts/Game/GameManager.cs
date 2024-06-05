using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UI;
using Players;
using Properties;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private List<Player> _players;
        private int _currentPlayerIndex;
        private Player _currentPlayer;

        private bool _isDiceRolled;
        private bool _gameHasStarted;
        private bool _isCasinoRoll;

        public Dice dice;
        public Button rollButton;
        public Button endTurnButton;
        public Button restartButton;
        public GameObject player1Prefab;
        public GameObject player2Prefab;
        public GameObject player3Prefab;
        public GameObject player4Prefab;
        public GameObject playerSelectionPanel; // Add this field

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
            rollButton.onClick.AddListener(RollDice);
            endTurnButton.onClick.AddListener(EndTurn);
            restartButton.onClick.AddListener(RestartGame);

            dice.OnDiceRollComplete = OnDiceRollComplete;

            _gameHasStarted = false;
            _isDiceRolled = false;
            _isCasinoRoll = false;
        }
        
        public void SetupGame(int humanPlayers, int aiPlayers)
        {
            _players = new List<Player>();

            for (int i = 0; i < humanPlayers; i++)
            {
                _players.Add(InstantiatePlayer(i, $"Player {i + 1}"));
            }

            for (int i = 0; i < aiPlayers; i++)
            {
                _players.Add(InstantiateAiPlayer(i, $"AI {i + 1}"));
            }

            _gameHasStarted = true;
            _currentPlayerIndex = 0;
            _currentPlayer = _players[_currentPlayerIndex];
            GameUI.SetPlayerInfo(_currentPlayer);
    
            GameUI.UnblockAll();
            GameUI.BlockEndTurnButton();
        }



        private void Update()
        {
            if (_gameHasStarted)
            {
                if (ConfirmationWindow.IsActive || CasinoUIManager.Instance.IsActive)
                {
                    GameUI.BlockAll();
                }
                else
                {
                    GameUI.UnblockAll();
                }
                
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
                
                if (_currentPlayer is AiPlayer)
                {
                    if (_isDiceRolled)
                    {
                        GameUI.BlockAll();
                        GameUI.UnblockEndTurnButton();
                    }
                    else
                    {
                        GameUI.BlockAll();
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space) && !_isDiceRolled)
                {
                    RollDice();
                }
            }
            else
            {
                GameUI.BlockAll();
            }
        }

        public void RollDice()
        {
            if (!_isDiceRolled)
            {
                _isCasinoRoll = false;
                dice.RollTheDice();
            }
        }

        public void RollDiceForCasino()
        {
            _isCasinoRoll = true;
            dice.RollTheDice();
        }

        private void OnDiceRollComplete()
        {
            _isDiceRolled = true;
            
            if (_isCasinoRoll)
            {
                CasinoUIManager.Instance.HandleCasinoRollComplete();
                return;
            }
            
            _currentPlayer.HandleOnDiceCompleted();
            _currentPlayer.HandleBankruptcy();
            
            GameUI.UpdatePlayerInfo();
        }
        
        public void EndTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            _currentPlayer = _players[_currentPlayerIndex];
            _isDiceRolled = false;
            GameUI.SetPlayerInfo(_currentPlayer);
            
            if (_currentPlayer is AiPlayer)
            {
                RollDice();
            }
        }
        
        public Player GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        // removes the player from the game
        public void RemovePlayer(Player player)
        {
            // resets the properties owned by the player
            foreach (var property in player.Properties)
            {
                property.ResetProperty();
            }

            // remove the player from the list
            _players.Remove(player);
            Destroy(player.gameObject);
            
            GameUI.ShowNotification($"{player.Name} is bankrupt! Removing from the game.");

            // check if the game is over
            if (_players.Count == 1)
            {
                GameUI.ShowNotification($"{_players[0].Name} wins the game! You can continue to play or restart the game.");
                GameUI.BlockRollButton();
                GameUI.BlockEndTurnButton();
                return;
            }

            // if the current player is the one being removed, move to the next player
            if (_currentPlayer == player)
            {
                EndTurn();
            }

            // update the current player index to ensure it is within bounds
            _currentPlayerIndex %= _players.Count;

            // update the UI
            GameUI.SetPlayerInfo(_players[_currentPlayerIndex]);
        }
        
        // instantiates a player prefab and initializes it
        private Player InstantiatePlayer(int playerIndex, string playerName)
        {
            GameObject playerObj;
            switch (playerIndex)
            {
                case 0:
                    playerObj = Instantiate(player1Prefab);
                    break;
                case 1:
                    playerObj = Instantiate(player2Prefab);
                    break;
                case 2:
                    playerObj = Instantiate(player3Prefab);
                    break;
                case 3:
                    playerObj = Instantiate(player4Prefab);
                    break;
                default:
                    playerObj = Instantiate(player1Prefab);
                    break;
            }
            Player player = playerObj.GetComponent<Player>();
            player.Initialize(playerName);
            return player;
        }

        
        private Player InstantiateAiPlayer(int playerIndex, string playerName)
        {
            GameObject playerObj;
            switch (playerIndex)
            {
                case 0:
                    playerObj = Instantiate(player1Prefab);
                    break;
                case 1:
                    playerObj = Instantiate(player2Prefab);
                    break;
                case 2:
                    playerObj = Instantiate(player3Prefab);
                    break;
                case 3:
                    playerObj = Instantiate(player4Prefab);
                    break;
                default:
                    playerObj = Instantiate(player1Prefab);
                    break;
            }
            AiPlayer aiPlayer = playerObj.AddComponent<AiPlayer>();
            aiPlayer.Initialize(playerName);
            return aiPlayer;
        }

        
        private void RestartGame()
        {
            // Reset properties and players
            foreach (var player in _players)
            {
                foreach (Property property in player.Properties)
                {
                    property.ResetProperty();
                }
                
                Destroy(player.gameObject);
            }

            dice.ResetDice();
            _players.Clear();

            // hide game UI elements
            GameUI.BlockAll();

            // show player selection panel
            playerSelectionPanel.GetComponent<PlayerSelectionManager>().ShowSelectionPanel();

            _gameHasStarted = false;
            _isDiceRolled = false;
            _isCasinoRoll = false;
        }
    }
}
