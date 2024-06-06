using System.Collections.Generic;
using Game;
using Players;
using Properties;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // this class is responsible for managing the property UI
    public class PropertyUIManager : MonoBehaviour
    {
        public static PropertyUIManager Instance;

        public GameObject propertyListPanel;
        public GameObject propertyItemPrefab;
        public Transform propertyListContent;
        public Button openPropertyListButton;

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
            openPropertyListButton.onClick.AddListener(TogglePropertyListPanel);
        }

        private void TogglePropertyListPanel()
        {
            propertyListPanel.SetActive(!propertyListPanel.activeSelf);
            if (propertyListPanel.activeSelf)
            {
                PopulatePropertyList();
            }
        }

        private void PopulatePropertyList()
        {
            // clear existing items
            foreach (Transform child in propertyListContent)
            {
                Destroy(child.gameObject);
            }

            // add new items
            Player currentPlayer = GameManager.Instance.GetCurrentPlayer();
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

                // find the buttons within the instantiated prefab
                Button upgradeButton = propertyItem.transform.Find("UpgradeButton").GetComponent<Button>();
                Button sellButton = propertyItem.transform.Find("SellButton").GetComponent<Button>();

                upgradeButton.onClick.AddListener(() => UpgradeProperty(property));
                sellButton.onClick.AddListener(() => SellProperty(property));
            }
        }

        private void UpgradeProperty(Property property)
        {
            property.UpgradeProperty();
            PopulatePropertyList(); // refresh the list to reflect changes
        }

        private void SellProperty(Property property)
        {
            property.SellProperty();
            PopulatePropertyList(); // refresh the list to reflect changes
        }
    }
}
