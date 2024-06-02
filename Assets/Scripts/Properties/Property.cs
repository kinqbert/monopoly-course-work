using Players;
using UI;
using UnityEngine;

namespace Properties
{
    public class Property
    {
        public string Name { get; }
        public int Price { get; }
        public int UpgradeLevel { get; private set; }
        public bool IsOwned => _owner != null;
        
        
        private GameParticipant _owner;
        private int _rent;
        private int UpgradeCost => Price / 2 * (UpgradeLevel + 1);

        
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
                GameUI.ShowNotification($"{player.Name} bought {Name} for ${Price}");
            }
        }
        
        public void SellProperty()
        {
            _owner.ModifyMoney(Price);
            _owner.RemoveProperty(this);
            GameUI.ShowNotification($"{_owner.Name} sold {Name} for {Price} money.");
            _owner = null;
            GameUI.UpdatePlayerInfo();
        }

        public void UpgradeProperty()
        {
            if (UpgradeLevel <= 5)
            {
                _owner.ModifyMoney(-UpgradeCost);
                UpgradeLevel++;
                GameUI.UpdatePlayerInfo();
            }
        }

        public void PayRent(GameParticipant player)
        {
            if (IsOwned && _owner != player)
            {
                int rentToPay = _rent + (UpgradeLevel * 10); // Example rent formula
                player.ModifyMoney(-rentToPay);
                _owner.ModifyMoney(rentToPay);
                GameUI.ShowNotification($"{player.Name} paid {rentToPay} rent to {_owner.Name} for {Name}.");
            }
        }
    }
}
