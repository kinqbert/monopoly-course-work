using TMPro;
using UI;
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
            GameUI.BlockAll();
        }

        private void OnStartGame()
        {
            int humanPlayers = humanPlayersDropdown.value;
            int aiPlayers = aiPlayersDropdown.value;

            if (humanPlayers + aiPlayers <= 4 && humanPlayers + aiPlayers >= 2)
            {
                GameManager.Instance.SetupGame(humanPlayers, aiPlayers);
                selectionPanel.SetActive(false); // Hide the panel
            }
            else if (humanPlayers == 0)
            {
                messageText.text = "Please select at least one human player!";
            }
            else if (humanPlayers + aiPlayers < 2)
            {
                messageText.text = "There must be at least two players!";
            }
            else if (humanPlayers + aiPlayers > 4)
            {
                messageText.text = "Total number of players cannot exceed 4!";
            }
        }

        public void ShowSelectionPanel()
        {
            GameUI.BlockAll();
            selectionPanel.SetActive(true);
            messageText.text = "";
            humanPlayersDropdown.value = 0;
            aiPlayersDropdown.value = 0;
        }
    }
}