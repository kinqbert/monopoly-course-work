using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlayerSelectionManager : MonoBehaviour
    {
        // references to different UI elements, set in the inspector
        public GameObject selectionPanel; 
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
            
            // game supports from 2 to 4 players, but it is easy to increase, it'll just be messy to rework animations
            if (humanPlayers + aiPlayers <= 4 && humanPlayers + aiPlayers >= 2)
            {
                GameManager.Instance.SetupGame(humanPlayers, aiPlayers);
                selectionPanel.SetActive(false); // hide the panel
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
            selectionPanel.SetActive(true);
            messageText.text = "";
            humanPlayersDropdown.value = 0;
            aiPlayersDropdown.value = 0;
        }
    }
}