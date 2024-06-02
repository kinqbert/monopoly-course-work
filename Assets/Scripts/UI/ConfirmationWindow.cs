using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class ConfirmationWindow : MonoBehaviour
    {
        public static ConfirmationWindow Instance;
        public static bool IsActive { get; private set; }

        public TextMeshProUGUI messageText; // reference to the text component that displays the message
        public Button yesButton; // reference to the button that confirms the action
        public Button noButton; // reference to the button that cancels the action

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

        /// <summary>
        /// Shows the confirmation window with the specified message and actions for yes and no.
        /// </summary>
        /// <param name="message">Message to be shown</param>
        /// <param name="onYes">Function to be completed on confirm</param>
        /// <param name="onNo">Function to be completed on cancel</param>
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
