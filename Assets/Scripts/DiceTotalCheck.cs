using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using TMPro;

public class DiceTotalCheck : MonoBehaviour
{
    private Dice _dice;
    
    private TextMeshProUGUI _diceTotalText;
    
    // Start is called before the first frame update
    void Start()
    {
        _dice = GameObject.Find("DiceManager").GetComponent<Dice>();
        _diceTotalText = GameObject.Find("Dice Total Text").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int[] currentValues = _dice.GetCurrentValues();
        
        if (_dice.GetDoneRolling() && !_dice.IsFirstRoll())
        {
            _diceTotalText.text = (currentValues[0] + currentValues[1]).ToString();
        }
        else
        {
            _diceTotalText.text = "";
        }
    }
}


