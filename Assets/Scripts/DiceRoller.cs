using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Sprite[] diceImage;
    private int[] currentValues = {0, 0};
    
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
        currentValues = Dice.RollDiceInts(); // using Dice class static methods from labs 
        Debug.Log("Rolled: " + currentValues[0] + " and " + currentValues[1] + ". Total: " + (currentValues[0] + currentValues[1]));

        if (Dice.IsDouble())
        {
            Debug.Log("Rolled a double!");
        }

        transform.GetChild(0).GetComponent<Image>().sprite = diceImage[currentValues[0] - 1]; // updating first dice image
        transform.GetChild(1).GetComponent<Image>().sprite = diceImage[currentValues[1] - 1]; // updating second dice image
    }
    
    public int[] GetCurrentValues()
    {
        return currentValues;
    }
}
