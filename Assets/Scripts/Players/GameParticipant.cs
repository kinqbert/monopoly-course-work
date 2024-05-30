using UnityEngine;

public class GameParticipant
{
    public string Name { get; }
    private int CurrentCellNumber { get; set; }

    public GameParticipant(string name)
    {
        Name = name;
        CurrentCellNumber = 0;
    }

    public void Move(int steps)
    {
        int previousCell = CurrentCellNumber;
        CurrentCellNumber = CurrentCellNumber + steps % Board.Board.CellsCount;
        Debug.Log($"{Name} moved to {CurrentCellNumber}");
    }
}