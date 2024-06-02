using Players;
using UI;
using UnityEngine;

namespace Properties
{
    public class Property
    {
        public string Name { get; }
        public int Price { get; }
        private int _rent;
        public GameParticipant Owner;

        public bool IsOwned => Owner != null;
        
        public Property(string name, int price, int rent)
        {
            Name = name;
            Price = price;
            _rent = rent;
            Owner = null;
        }

        public void BuyProperty(GameParticipant player)
        {
            if (!IsOwned && player.Money >= Price)
            {
                player.ModifyMoney(-Price);
                Owner = player;
                player.AddProperty(this);
                Debug.Log($"{player.Name} bought {Name} for {Price} money.");
            }
            else if (IsOwned)
            {
                Debug.Log($"{Name} is already owned by {Owner.Name}.");
            }
            else
            {
                Debug.Log($"{player.Name} does not have enough money to buy {Name}.");
            }
        }
        
        public void SellProperty()
        {
            if (IsOwned)
            {
                Owner.ModifyMoney(Price);
                Owner.RemoveProperty(this);
                Debug.Log($"{Owner.Name} sold {Name} for {Price} money.");
                Owner = null;
                GameUI.UpdatePlayerInfo();
            }
        }

        public void PayRent(GameParticipant player)
        {
            if (IsOwned && Owner != player)
            {
                player.ModifyMoney(-_rent);
                Owner.ModifyMoney(_rent);
                Debug.Log($"{player.Name} paid {_rent} rent to {Owner.Name} for {Name}.");
            }
        }
    }
}