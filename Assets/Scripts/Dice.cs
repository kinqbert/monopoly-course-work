using System;

public static class Dice
{
    private static readonly Random Random = new Random();
    
    public static int[] RollDiceInts(int numberOfDice)
    {
        var result = new int[numberOfDice];

        for (var i = 0; i < numberOfDice; i++)
        {
            result[i] = RollDice();
        }

        return result;
    }
    
    public static int RollDice()
    {
        return Random.Next(1, 13);
    }
    
    public static bool IsDouble()
    {
        var dice = RollDiceInts(2);
        return dice[0] == dice[1];
    }
}