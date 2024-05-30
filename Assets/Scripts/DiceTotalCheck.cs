using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using TMPro;

public class DiceTotalCheck : MonoBehaviour
{
    private DiceRoller _diceRoller;
    
    private TextMeshProUGUI _diceTotalText;
    
    // Start is called before the first frame update
    void Start()
    {
        _diceRoller = FindObjectOfType<DiceRoller>();
    }

    // Update is called once per frame
    void Update()
    {
        int[] currentValues = _diceRoller.GetCurrentValues();
        
        if (_diceRoller.GetDoneRolling() && !_diceRoller.IsFirstRoll())
        {
            _diceTotalText.text = (currentValues[0] + currentValues[1]).ToString();
        }
        else
        {
            _diceTotalText.text = "";
        }
    }
}


