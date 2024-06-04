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

        private Player _player;

        public void SetPlayerInfo(Player player) {
            _player = player;

            if (player.IsInJail)
            {
                playerNameText.text = "[IN JAIL] " + player.Name;
            }
            else
            {
                playerNameText.text = player.Name;
            }
            
            playerMoneyText.text = "Money: " + player.Money;
            playerPropertiesText.text = "Properties amount: " + player.Properties.Count;
        }

        public void UpdatePlayerInfo()
        {
            playerMoneyText.text = "Money: " + _player.Money;
            playerPropertiesText.text = "Properties amount: " + _player.Properties.Count;
        }
    }
}
