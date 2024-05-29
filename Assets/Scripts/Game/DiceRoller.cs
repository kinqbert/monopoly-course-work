using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Sprite[] diceImage;
    private int[] _currentValues = {0, 0};
    public float animationDuration = 1.5f; // duration of the dice roll animation
    private bool _doneRolling = true;
    private bool _isFirstRoll = true;

    private Image _diceImage1;
    private Image _diceImage2;
    private Button _rollButton;

    void Start()
    {
        // Saving references to the dice images
        _diceImage1 = transform.GetChild(0).GetComponent<Image>();
        _diceImage2 = transform.GetChild(1).GetComponent<Image>();

        // Finding the Roll button in the scene
        _rollButton = GameObject.Find("RollDice-Button").GetComponent<Button>();
    }

    public void RollTheDice()
    {
        if (_doneRolling)
        {
            StartCoroutine(RollDiceAnimation());
        }
    }

    private IEnumerator RollDiceAnimation()
    {
        _isFirstRoll = false;
        _doneRolling = false;
        _rollButton.interactable = false; // Disable the Roll button
        float elapsed = 0.0f;
        while (elapsed < animationDuration)
        {
            int randomIndex1 = Random.Range(0, diceImage.Length);
            int randomIndex2 = Random.Range(0, diceImage.Length);

            _diceImage1.sprite = diceImage[randomIndex1];
            _diceImage2.sprite = diceImage[randomIndex2];

            elapsed += Time.deltaTime;
            yield return null; // wait for the next frame
        }
        _doneRolling = true;
        _rollButton.interactable = true; // Enable the Roll button
        
        _currentValues = Dice.RollDiceInts();
        _diceImage1.sprite = diceImage[_currentValues[0] - 1];
        _diceImage2.sprite = diceImage[_currentValues[1] - 1];
    }

    public int[] GetCurrentValues()
    {
        return _currentValues;
    }

    public bool GetDoneRolling()
    {
        return _doneRolling;
    }

    public bool IsFirstRoll()
    {
        return _isFirstRoll;
    }
}


