using Players;
using UnityEngine;
using TMPro;

namespace UI
{
    public class PlayerInfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI playerNameText;
        public TextMeshProUGUI playerMoneyText;
        public TextMeshProUGUI playerPropertiesText;
        
        public GameParticipant Player { get; private set; }

        public void SetPlayerInfo(GameParticipant player) {
            Player = player;
            
            playerNameText.text = player.Name;
            playerMoneyText.text = "Money: " + player.Money;
            playerPropertiesText.text = "Properties amount: " + player.Properties.Count;
        }

        public void UpdatePlayerInfo()
        {
            playerMoneyText.text = "Money: " + Player.Money;
            playerPropertiesText.text = "Properties amount: " + Player.Properties.Count;
        }
    }
}
