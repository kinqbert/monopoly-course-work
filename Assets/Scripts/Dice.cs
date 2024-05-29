using System;

public static class Dice
{
    private static readonly Random Random = new Random();
    private static readonly int[] CurrentValues = {0, 0};
    
    public static int[] RollDiceInts()
    {
        for (var i = 0; i < 2; i++)
        {
            CurrentValues[i] = Random.Next(1, 7);
        }

        return CurrentValues;
    }
    
    public static bool IsDouble()
    {
        return CurrentValues[0] == CurrentValues[1];
    }

    public static int[] GetCurrentValues()
    {
        return CurrentValues;
    }
}