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
                propertyText.text = $"{property.Name} - ${property.Price}";
                propertyItem.GetComponent<Button>().onClick.AddListener(() => SellProperty(property));
            }
        }

        private void SellProperty(Property property)
        {
            GameParticipant currentPlayer = GameManager.Instance.GetCurrentPlayer();
            property.SellProperty();
            GameUI.ShowNotification($"{currentPlayer.Name} sold {property.Name} for ${property.Price}");
            PopulatePropertyList();
        }
    }
}
