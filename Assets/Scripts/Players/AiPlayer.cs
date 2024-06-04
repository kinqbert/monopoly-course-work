using System.Collections;

using Fields;
using Game;
using Properties;
using UI;
using UnityEngine;

namespace Players
{
    public class AiPlayer : Player
    {
        private const int MinimumMoneyToBuyProperty = 200;
        private const int MinimumMoneyToUpgradeProperty = 300;

        public override void HandleOnDiceCompleted()
        {
            if (IsInJail)
            {
                HandleJail();
            }
            else
            {
                HandleTurn();
            }
        }
        
        private void HandleTurn()
        {
            Move(GameManager.Instance.dice.GetTotal());
            
            CurrentTile.Field.OnPlayerLanded(this);

            if (CurrentTile.Field is PropertyField propertyField)
            {
                DecideToBuyProperty(propertyField.Property);
            }

            DecideToUpgradeProperties();
        }

        private void DecideToBuyProperty(Property property)
        {
            if (!property.IsOwned && Money >= property.Price + MinimumMoneyToBuyProperty)
            {
                property.BuyProperty(this);
            }
        }

        private void DecideToUpgradeProperties()
        {
            foreach (var property in Properties)
            {
                if (Money >= property.UpgradeCost + MinimumMoneyToUpgradeProperty && property.UpgradeLevel < 5)
                {
                    property.UpgradeProperty();
                }
            }
        }
        
        public void HandleCasino()
        {
            StartCoroutine(PlaceRandomBet());
        }

        private IEnumerator PlaceRandomBet()
        {
            yield return new WaitForSeconds(1); // simulate decision making time

            string[] betTypes = { "odd", "even", "double" };
            string betType = betTypes[Random.Range(0, betTypes.Length)];
            int betAmount = Random.Range(1, Mathf.Min(Money, 100)); // random bet amount up to $100 or player's balance

            CasinoUIManager.Instance.PlaceBet(this, betType, betAmount);
        }
    }
}
