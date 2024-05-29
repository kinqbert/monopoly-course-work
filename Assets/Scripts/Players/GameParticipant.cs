using UnityEngine;

public class GameParticipant
{
    public string Name { get; }
    public int CurrentCell { get; private set; }

    public GameParticipant(string name)
    {
        Name = name;
        CurrentCell = 0;
    }

    public void Move(int steps)
    {
        int previousCell = CurrentCell;
        CurrentCell = (CurrentCell + steps) % 40; // Assuming 40 cells on the board
        Debug.Log($"{Name} moved from {previousCell} to {CurrentCell} ({Board.Board.GetInstance().GetCell(CurrentCell)})");
    }
}