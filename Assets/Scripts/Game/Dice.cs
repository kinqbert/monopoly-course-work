using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Dice : MonoBehaviour
    {
        public Sprite[] diceImages;
        private int[] _currentValues = { 0, 0 };
        public float animationDuration = 1.5f; // duration of the dice roll animation
        private bool _doneRolling = true;
        private bool _isFirstRoll = true;

        private Image _diceImage1;
        private Image _diceImage2;
        private Button _rollButton;

        private static readonly System.Random Random = new System.Random();
        
        public Action OnDiceRollComplete; // Add this action

        void Start() {
            // saving references to the dice images
            _diceImage1 = GameObject.Find("Dice-Image-1").GetComponent<Image>();
            _diceImage2 = GameObject.Find("Dice-Image-2").GetComponent<Image>();

            // finding the Roll button in the scene
            _rollButton = GameObject.Find("Roll-Dice-Button").GetComponent<Button>();
        }

        public void RollTheDice() {
            if (_doneRolling) {
                _isFirstRoll = false;
                _doneRolling = false;
                _rollButton.interactable = false; // disabling the Roll button

                StartCoroutine(DiceRolling());
            }
        }

        private IEnumerator DiceRolling() 
        {
            float elapsed = 0.0f; // time elapsed from the start of the animation
            while (elapsed < animationDuration) {
                
                int randomIndex1 = UnityEngine.Random.Range(0, diceImages.Length);
                int randomIndex2 = UnityEngine.Random.Range(0, diceImages.Length);

                _diceImage1.sprite = diceImages[randomIndex1];
                _diceImage2.sprite = diceImages[randomIndex2];

                elapsed += Time.deltaTime;
                yield return null; // wait for the next frame
            }

            _currentValues = RollDiceInts(); // getting final value of the dice roll
            _diceImage1.sprite = diceImages[_currentValues[0] - 1]; // setting the final dice image 1
            _diceImage2.sprite = diceImages[_currentValues[1] - 1];

            _doneRolling = true;
            _rollButton.interactable = true; // enabling the Roll button again
            
            OnDiceRollComplete?.Invoke(); // Call the completion action
        }

        private int[] RollDiceInts() 
        {
            for (var i = 0; i < 2; i++) {
                _currentValues[i] = RollSingleDice();
            }
            return _currentValues;
        }

        private int RollSingleDice() {
            return Random.Next(1, 7);
        }

        public bool IsDouble() {
            return _currentValues[0] == _currentValues[1];
        }

        public int[] GetCurrentValues() {
            return _currentValues;
        }

        public int GetTotal() {
            return _currentValues[0] + _currentValues[1];
        }

        public bool GetDoneRolling() {
            return _doneRolling;
        }

        public bool IsFirstRoll() {
            return _isFirstRoll;
        }
    }
}
