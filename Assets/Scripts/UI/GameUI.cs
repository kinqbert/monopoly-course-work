using Players;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // this class is responsible for managing all of the game UI
    public class GameUI
    {
        // all of the needed buttons and panels in the game
        private static readonly Button RollButton = GameObject.Find("Roll-Dice-Button").GetComponent<Button>();
        private static readonly Button EndTurnButton = GameObject.Find("End-Turn-Button").GetComponent<Button>();
        private static readonly Button OpenPropertyListButton = GameObject.Find("Open-Property-List-Button").GetComponent<Button>();
        private static readonly Button RestartGameButton = GameObject.Find("Restart-Button").GetComponent<Button>();
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
        
        public static void BlockEndTurnButton()
        {
            EndTurnButton.interactable = false;
        }
        
        public static void UnblockEndTurnButton()
        {
            EndTurnButton.interactable = true;
        }
        
        public static void BlockAll()
        {
            RollButton.interactable = false;
            EndTurnButton.interactable = false;
            OpenPropertyListButton.interactable = false;
            RestartGameButton.interactable = false;
        }
        
        public static void UnblockAll()
        {
            RollButton.interactable = true;
            EndTurnButton.interactable = true;
            OpenPropertyListButton.interactable = true;
            RestartGameButton.interactable = true;
        }
        
        public static void ShowNotification(string message)
        {
            NotificationPanel.Instance.ShowNotification(message);
        }
        
        public static void UpdatePlayerInfo()
        {
            PlayerInfoPanel.UpdatePlayerInfo();
        }
        
        public static void SetPlayerInfo(Player player)
        {
            PlayerInfoPanel.SetPlayerInfo(player);
        }
    }
}
