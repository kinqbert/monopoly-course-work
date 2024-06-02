using Players;
using UnityEngine;

namespace Properties
{
    public class Property
    {
        public string Name { get; }
        public int Price { get; }
        private int _rent;
        private GameParticipant _owner;

        public bool IsOwned => _owner != null;
        
        public Property(string name, int price, int rent)
        {
            Name = name;
            Price = price;
            _rent = rent;
            _owner = null;
        }

        public void BuyProperty(GameParticipant player)
        {
            if (!IsOwned && player.Money >= Price)
            {
                player.ModifyMoney(-Price);
                _owner = player;
                player.AddProperty(this);
                Debug.Log($"{player.Name} bought {Name} for {Price} money.");
            }
            else if (IsOwned)
            {
                Debug.Log($"{Name} is already owned by {_owner.Name}.");
            }
            else
            {
                Debug.Log($"{player.Name} does not have enough money to buy {Name}.");
            }
        }

        public void PayRent(GameParticipant player)
        {
            if (IsOwned && _owner != player)
            {
                player.ModifyMoney(-_rent);
                _owner.ModifyMoney(_rent);
                Debug.Log($"{player.Name} paid {_rent} rent to {_owner.Name} for {Name}.");
            }
        }
    }
}