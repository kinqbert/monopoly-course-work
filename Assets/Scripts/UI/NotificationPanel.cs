using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    // this class manages the notification panel that displays messages to the player
    public class NotificationPanel : MonoBehaviour
    {
        public static NotificationPanel Instance;

        public GameObject notificationPrefab;
        public float displayDuration = 3f;
        public float fadeDuration = 1f; // duration for the fade-out effect

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
        }

        public void ShowNotification(string message)
        {
            GameObject notification = Instantiate(notificationPrefab, transform);
            TextMeshProUGUI notificationText = notification.GetComponentInChildren<TextMeshProUGUI>();
            notificationText.text = message;
            StartCoroutine(HideNotificationAfterDelay(notification, displayDuration));
        }

        private IEnumerator HideNotificationAfterDelay(GameObject notification, float delay)
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(FadeOutAndDestroy(notification));
        }

        // crazy algoithm ahead
        private IEnumerator FadeOutAndDestroy(GameObject notification)
        {
            CanvasGroup canvasGroup = notification.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = notification.AddComponent<CanvasGroup>();
            }

            float startAlpha = canvasGroup.alpha;
            float rate = 1.0f / fadeDuration;
            float progress = 0.0f;

            while (progress < 1.0f)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 0;
            Destroy(notification);
        }
    }
}