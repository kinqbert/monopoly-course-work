using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private Board.Board _gameBoard;
    
    private List<GameParticipant> _players;
    private int _currentPlayerIndex;
    private GameParticipant _currentPlayer;
    
    private bool _isDiceRolled;
    private DiceRoller _diceRoller;

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
    }

    void Update()
    {
        // Check if the space bar was pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _diceRoller.RollTheDice();
        }
    }
}