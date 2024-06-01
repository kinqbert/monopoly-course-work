using Players;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUI
    {
        private static readonly Button RollButton = GameObject.Find("Roll-Dice-Button").GetComponent<Button>();
        private static readonly Button EndTurnButton = GameObject.Find("End-Turn-Button").GetComponent<Button>();
        private static readonly PlayerInfoPanel PlayerInfoPanel = GameObject.Find("Player-Info-Panel").GetComponent<PlayerInfoPanel>();

        public static void YesNoWindow(string message, System.Action onYes, System.Action onNo)
        {
            BlockAll();
            ConfirmationWindow.Instance.Show(message, () =>
            {
                onYes();
                UnblockAll();
            }, () =>
            {
                onNo();
                UnblockAll();
            });
        }
        
        public static void BlockRollButton()
        {
            RollButton.interactable = false;
        }
        
        public static void UnblockRollButton()
        {
            RollButton.interactable = true;
        }
        
        public static void BlockAll()
        {
            RollButton.interactable = false;
            EndTurnButton.interactable = false;
        }
        
        public static void UnblockAll()
        {
            RollButton.interactable = true;
            EndTurnButton.interactable = true;
        }
        
        public static void UpdatePlayerInfo()
        {
            PlayerInfoPanel.UpdatePlayerInfo();
        }
        
        public static void SetPlayerInfo(GameParticipant player)
        {
            PlayerInfoPanel.SetPlayerInfo(player);
        }
    }
}