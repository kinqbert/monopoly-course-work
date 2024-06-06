using Game;
using UnityEngine;
using TMPro;

namespace UI
{
    // this class is responsible for displaying the total of the dice roll
    public class DiceTotalCheck : MonoBehaviour
    {
        private Dice _dice;
        private TextMeshProUGUI _diceTotalText;
    
        void Start()
        {
            _dice = GameObject.Find("DiceManager").GetComponent<Dice>();
            _diceTotalText = GameObject.Find("Dice Total Text").GetComponent<TextMeshProUGUI>();
        }

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
}



