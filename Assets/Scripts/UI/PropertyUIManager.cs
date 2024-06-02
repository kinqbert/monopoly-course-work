using System.Collections.Generic;
using Game;
using Players;
using Properties;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PropertyUIManager : MonoBehaviour
    {
        public static PropertyUIManager Instance;

        public GameObject propertyListPanel;
        public GameObject propertyItemPrefab;
        public Transform propertyListContent;
        public Button openPropertyListButton;
        public Button openUpgradeListButton;

        private bool _isUpgradeMode;

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

        private void Start()
        {
            propertyListPanel.SetActive(false);
            openPropertyListButton.onClick.AddListener(() => TogglePropertyListPanel(false));
            openUpgradeListButton.onClick.AddListener(() => TogglePropertyListPanel(true));
        }

        public void TogglePropertyListPanel(bool upgradeMode)
        {
            _isUpgradeMode = upgradeMode;
            propertyListPanel.SetActive(!propertyListPanel.activeSelf);
            if (propertyListPanel.activeSelf)
            {
                PopulatePropertyList();
            }
        }

        private void PopulatePropertyList()
        {
            // Clear existing items
            foreach (Transform child in propertyListContent)
            {
                Destroy(child.gameObject);
            }

            // Add new items
            GameParticipant currentPlayer = GameManager.Instance.GetCurrentPlayer();
            List<Property> properties = currentPlayer.Properties;

            foreach (Property property in properties)
            {
                GameObject propertyItem = Instantiate(propertyItemPrefab, propertyListContent);
                TextMeshProUGUI propertyText = propertyItem.GetComponentInChildren<TextMeshProUGUI>();

                if (property.UpgradeLevel == 5)
                {
                    propertyText.text = $"{property.Name} - Rent: ${property.Rent} - ${property.Price} - Level MAX";
                }
                else
                {
                    propertyText.text = $"{property.Name} - Rent: ${property.Rent} - ${property.Price} - Level: {property.UpgradeLevel} - Upgrade Cost: ${property.UpgradeCost}";
                }
                
                if (_isUpgradeMode)
                {
                    propertyItem.GetComponent<Button>().onClick.AddListener(() => UpgradeProperty(property));
                }
                else
                {
                    propertyItem.GetComponent<Button>().onClick.AddListener(() => SellProperty(property));
                }
            }
        }

        private void SellProperty(Property property)
        {
            GameParticipant currentPlayer = GameManager.Instance.GetCurrentPlayer();
            property.SellProperty();
            GameUI.ShowNotification($"{currentPlayer.Name} sold {property.Name} for ${property.Price}");
            PopulatePropertyList();
        }

        private void UpgradeProperty(Property property)
        {
            property.UpgradeProperty();
            PopulatePropertyList();
        }
    }
}
