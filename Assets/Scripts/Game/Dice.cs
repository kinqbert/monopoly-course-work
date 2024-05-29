using System;

public static class Dice
{
    private static readonly Random Random = new Random();
    private static readonly int[] currentValues = new int[2];

    public static int[] RollDiceInts()
    {
        for (var i = 0; i < 2; i++)
        {
            currentValues[i] = RollDice();
        }
        return currentValues;
    }

    public static int RollDice()
    {
        return Random.Next(1, 7);
    }

    public static bool IsDouble()
    {
        return currentValues[0] == currentValues[1];
    }

    public static int[] CurrentValues()
    {
        return currentValues;
    }
}
