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

        private GameParticipant _player;

        public void SetPlayerInfo(GameParticipant player) {
            _player = player;
            
            playerNameText.text = player.Name;
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
