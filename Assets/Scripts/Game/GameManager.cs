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

        private List<Player> _players; // list of players in the game
        private int _currentPlayerIndex;  
        private Player _currentPlayer; 

        private bool _isDiceRolled;
        private bool _gameHasStarted; 
        private bool _isCasinoRoll; // variable to check if the dice roll is for the casino, if it is then player isn't moving

        public Dice dice; // Reference to the dice object
        
        // button references
        public Button rollButton;
        public Button endTurnButton;
        public Button restartButton;
        
        // player prefabs to make different players have different models
        public GameObject player1Prefab;
        public GameObject player2Prefab;
        public GameObject player3Prefab;
        public GameObject player4Prefab;
        
        // reference to the player selection panel
        public GameObject playerSelectionPanel;

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
            // adding listeners to the buttons
            rollButton.onClick.AddListener(RollDice);
            endTurnButton.onClick.AddListener(EndTurn);
            restartButton.onClick.AddListener(RestartGame);

            // assigning function to be called when the dice roll is complete
            dice.OnDiceRollComplete = OnDiceRollComplete;

            _gameHasStarted = false;
            _isDiceRolled = false;
            _isCasinoRoll = false;
        }
        
        // sets up the game with the given number of human and AI players
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
        }

        private void Update()
        {
            // block diffrent UI elements based on the game state
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

        // method to roll the dice
        public void RollDice()
        {
            if (!_isDiceRolled)
            {
                _isCasinoRoll = false;
                dice.RollTheDice();
            }
        }

        // method to roll the dice for the casino, it won't move player
        public void RollDiceForCasino()
        {
            _isCasinoRoll = true;
            dice.RollTheDice();
        }

        // method to be called when the dice roll is complete (passed into dice class in Start function)
        private void OnDiceRollComplete()
        {
            _isDiceRolled = true;
            
            // if the dice roll is for the casino, give or take money from the player
            if (_isCasinoRoll)
            {
                CasinoUIManager.Instance.HandleCasinoRollComplete();
                return;
            }
            
            
            _currentPlayer.HandleOnDiceCompleted(); // move the player and handle the tile they landed on if needed or reduce jail turns
            
            // update the player info on the UI
            GameUI.UpdatePlayerInfo();
            
            // check if the player is bankrupt
            if (_currentPlayer.IsBankrupt())
            {
                RemovePlayer(_currentPlayer);
            } 
        }
        
        public void EndTurn()
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
            _currentPlayer = _players[_currentPlayerIndex];
            _isDiceRolled = false;
            
            // set new player info on the UI
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

            // just in case if the current player is the one being removed, move to the next player
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
            
            // instantiate the player prefab (model) based on the player index
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
            // add the player script to the player object and initialize it
            Player player = playerObj.GetComponent<Player>();
            player.Initialize(playerName);
            
            return player;
        }

        // same as InstantiatePlayer but for AI players
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

        // method to restart the game
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

            // reset the dice and players list
            dice.ResetDice();
            _players.Clear();

            // show player selection panel
            playerSelectionPanel.GetComponent<PlayerSelectionManager>().ShowSelectionPanel();

            _gameHasStarted = false;
            _isDiceRolled = false;
            _isCasinoRoll = false;
        }
    }
}
