using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameUI
    {
        private static Button _rollButton = GameObject.Find("Roll-Dice-Button").GetComponent<Button>();

        public static void YesNoWindow(string message, System.Action onYes, System.Action onNo)
        {
            BlockAll();
            ConfirmationWindow.Instance.Show(message, () =>
            {
                onYes();
                UnblockAll();
            }, () =>
            {
                onNo();
                UnblockAll();
            });
        }
        
        public static void BlockAll()
        {
            _rollButton.interactable = false;
        }
        
        public static void UnblockAll()
        {
            _rollButton.interactable = true;
        }
    }
}