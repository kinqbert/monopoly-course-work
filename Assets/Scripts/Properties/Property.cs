using Players;
using UnityEngine;

namespace Properties
{
    public class Property
    {
        public string propertyName { get; }
        public int cost { get; }
        public int rent { get; }
        public GameParticipant owner;

        public bool IsOwned => owner != null;
        
        public Property(string name, int cost, int rent)
        {
            propertyName = name;
            this.cost = cost;
            this.rent = rent;
            owner = null;
        }

        public void BuyProperty(GameParticipant player)
        {
            if (!IsOwned && player.Money >= cost)
            {
                player.ModifyMoney(-cost);
                owner = player;
                player.AddProperty(this);
                Debug.Log($"{player.Name} bought {propertyName} for {cost} money.");
            }
            else if (IsOwned)
            {
                Debug.Log($"{propertyName} is already owned by {owner.Name}.");
            }
            else
            {
                Debug.Log($"{player.Name} does not have enough money to buy {propertyName}.");
            }
        }

        public void PayRent(GameParticipant player)
        {
            if (IsOwned && owner != player)
            {
                player.ModifyMoney(-rent);
                owner.ModifyMoney(rent);
                Debug.Log($"{player.Name} paid {rent} rent to {owner.Name} for {propertyName}.");
            }
        }
    }
}