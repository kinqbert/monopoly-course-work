using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Dice : MonoBehaviour
    {
        public Sprite[] diceImages; // every possible image dice can have
        private int[] _currentValues = { 0, 0 }; // current values of the dice roll. 0 means not rolled yet
        public float animationDuration = 1.5f; // duration of the dice roll animation
        private bool _doneRolling = true; 
        private bool _isFirstRoll = true;

        private Image _diceImage1; // references to first and second dice images respectively
        private Image _diceImage2;

        private static readonly System.Random Random = new (); 
        
        public Action OnDiceRollComplete; // action to be called when the dice roll is complete

        void Start() {
            // saving references to the dice images
            _diceImage1 = GameObject.Find("Dice-Image-1").GetComponent<Image>();
            _diceImage2 = GameObject.Find("Dice-Image-2").GetComponent<Image>();
            
            _diceImage1.sprite = diceImages[0];
            _diceImage2.sprite = diceImages[0];
        }

        // method to roll the dice with animation
        public void RollTheDice() {
            if (_doneRolling) {
                _isFirstRoll = false;
                _doneRolling = false;

                StartCoroutine(DiceRolling());
            }
        }

        private IEnumerator DiceRolling() 
        {
            float elapsed = 0.0f; // time elapsed from the start of the animation
            
            // randomizing dice images until the animation duration is reached
            while (elapsed < animationDuration) {
                
                int randomIndex1 = UnityEngine.Random.Range(0, diceImages.Length);
                int randomIndex2 = UnityEngine.Random.Range(0, diceImages.Length);

                _diceImage1.sprite = diceImages[randomIndex1];
                _diceImage2.sprite = diceImages[randomIndex2];

                elapsed += Time.deltaTime;
                yield return null; // wait for the next frame
            }

            _currentValues = RollDiceInts(); // getting final value of the dice roll
            _diceImage1.sprite = diceImages[_currentValues[0]]; // setting the final dice image 1
            _diceImage2.sprite = diceImages[_currentValues[1]];

            _doneRolling = true;
            
            OnDiceRollComplete?.Invoke(); // call the completion action
        }

        // method to roll the dice without animation, being used privately
        private int[] RollDiceInts() 
        {
            for (var i = 0; i < 2; i++) {
                _currentValues[i] = Random.Next(1, 7);
            }
            
            return _currentValues;
        }

        // method to check if current dice values are double
        public bool IsDouble() {
            return _currentValues[0] == _currentValues[1];
        }

        // method to get current dice values
        public int[] GetCurrentValues() {
            return _currentValues;
        }

        // method to get the total value of current dice values
        public int GetTotal() {
            return _currentValues[0] + _currentValues[1];
        }

        // method to check if the dice roll is done (used for a casino)
        public bool GetDoneRolling() {
            return _doneRolling;
        }

        // method to check if the current roll is the first roll (used for dice text field)
        public bool IsFirstRoll() {
            return _isFirstRoll;
        }
        
        // method to reset the dice to the initial state (used when game is restarted)
        public void ResetDice() {
            _isFirstRoll = true;
            
            _currentValues = new [] { 0, 0 };
            
            _diceImage1.sprite = diceImages[0];
            _diceImage2.sprite = diceImages[0];
        }
    }
}
