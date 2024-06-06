using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    // this class manages the confirmation window that asks the player to confirm an action
    public class ConfirmationWindow : MonoBehaviour
    {
        public static ConfirmationWindow Instance;
        public static bool IsActive { get; private set; }

        public TextMeshProUGUI messageText; // reference to the text component that displays the message
        
        // references to the buttons that confirm and cancel the action respectively
        public Button yesButton; 
        public Button noButton;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            gameObject.SetActive(false); // initially hide the confirmation window
        }

        // shows the confirmation window with the specified message and actions for yes and no.
        public void Show(string message, System.Action onYes, System.Action onNo)
        {
            IsActive = true;
            messageText.text = message;
            gameObject.SetActive(true);

            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();

            yesButton.onClick.AddListener(() => {
                onYes?.Invoke();
                gameObject.SetActive(false);
                IsActive = false;
            });

            noButton.onClick.AddListener(() => {
                onNo?.Invoke();
                gameObject.SetActive(false);
                IsActive = false;
            });
        }
    }
}
