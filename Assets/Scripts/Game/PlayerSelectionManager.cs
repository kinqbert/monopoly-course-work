using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerSelectionManager : MonoBehaviour
    {
        public GameObject selectionPanel; // Reference to the panel
        public TextMeshProUGUI messageText;
        public TMP_Dropdown humanPlayersDropdown;
        public TMP_Dropdown aiPlayersDropdown;
        public Button startButton;

        private void Start()
        {
            startButton.onClick.AddListener(OnStartGame);
        }

        private void OnStartGame()
        {
            int humanPlayers = humanPlayersDropdown.value;
            int aiPlayers = aiPlayersDropdown.value;

            if (humanPlayers + aiPlayers <= 4)
            {
                GameManager.Instance.SetupGame(humanPlayers, aiPlayers);
                selectionPanel.SetActive(false); // Hide the panel
            }
            else
            {
                messageText.text = "Total number of players cannot exceed 4!";
            }
        }
    }
}