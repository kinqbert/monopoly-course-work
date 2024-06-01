using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class ConfirmationWindow : MonoBehaviour
    {
        public static ConfirmationWindow Instance;
        public static bool IsActive { get; private set; }

        public TextMeshProUGUI messageText;
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

            gameObject.SetActive(false); // Initially hide the confirmation window
        }

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
