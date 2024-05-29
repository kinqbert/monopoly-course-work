using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Sprite[] DiceImage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollTheDice()
    {
        int[] diceRolls = Dice.RollDiceInts(); // using Dice class static methods from labs 
        Debug.Log("Rolled: " + diceRolls[0] + " and " + diceRolls[1] + ". Total: " + (diceRolls[0] + diceRolls[1]));

        if (Dice.IsDouble())
        {
            Debug.Log("Rolled a double!");
        }

        this.transform.GetChild(0).GetComponent<Image>().sprite = DiceImage[diceRolls[0] - 1]; // updating first dice image
        this.transform.GetChild(1).GetComponent<Image>().sprite = DiceImage[diceRolls[1] - 1]; // updating second dice image
    }
}
