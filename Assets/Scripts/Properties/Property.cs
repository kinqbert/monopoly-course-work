using Players;
using UnityEngine;

namespace Properties
{
    public class Property
    {
        public string PropertyName { get; }
        public int Cost { get; }
        private int _rent;
        private GameParticipant _owner;

        public bool IsOwned => _owner != null;
        
        public Property(string name, int cost, int rent)
        {
            PropertyName = name;
            Cost = cost;
            _rent = rent;
            _owner = null;
        }

        public void BuyProperty(GameParticipant player)
        {
            if (!IsOwned && player.Money >= Cost)
            {
                player.ModifyMoney(-Cost);
                _owner = player;
                player.AddProperty(this);
                Debug.Log($"{player.Name} bought {PropertyName} for {Cost} money.");
            }
            else if (IsOwned)
            {
                Debug.Log($"{PropertyName} is already owned by {_owner.Name}.");
            }
            else
            {
                Debug.Log($"{player.Name} does not have enough money to buy {PropertyName}.");
            }
        }

        public void PayRent(GameParticipant player)
        {
            if (IsOwned && _owner != player)
            {
                player.ModifyMoney(-_rent);
                _owner.ModifyMoney(_rent);
                Debug.Log($"{player.Name} paid {_rent} rent to {_owner.Name} for {PropertyName}.");
            }
        }
    }
}