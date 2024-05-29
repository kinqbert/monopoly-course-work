using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceTotalCheck : MonoBehaviour
{
    private DiceRoller _diceRoller;
    
    // Start is called before the first frame update
    void Start()
    {
        _diceRoller = FindObjectOfType<DiceRoller>();
    }

    // Update is called once per frame
    void Update()
    {
        int[] currentValues = _diceRoller.GetCurrentValues();
        
        if (_diceRoller.GetIsRolling())
        {
            GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = (currentValues[0] + currentValues[1]).ToString();
        }
    }
}
