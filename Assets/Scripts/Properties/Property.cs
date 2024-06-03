using Players;
using UI;

namespace Properties
{
    public class Property
    {
        public string Name { get; }
        public int Price => _initialPrice + (int)(_initialPrice * 0.25 * (UpgradeLevel - 1)); // Dynamic price calculation
        public int UpgradeLevel { get; private set; }
        public bool IsOwned => _owner != null;
        public int UpgradeCost => _initialPrice / 10 + UpgradeLevel * 50;
        public int Rent => _initialPrice / 10 * UpgradeLevel;

        private int _initialPrice;
        private GameParticipant _owner;

        public Property(string name, int price)
        {
            Name = name;
            UpgradeLevel = 1; // Start with level 1
            _initialPrice = price;
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
            int sellPrice = Price; // Reset to initial price on sell
            _owner.ModifyMoney(sellPrice);
            _owner.RemoveProperty(this);
            UpgradeLevel = 1; // Reset to level 1
            GameUI.ShowNotification($"{_owner.Name} sold {Name} for ${sellPrice} money.");
            _owner = null;
            GameUI.UpdatePlayerInfo();
        }

        public void UpgradeProperty()
        {
            if (UpgradeLevel < 5 && _owner.Money >= UpgradeCost)
            {
                _owner.ModifyMoney(-UpgradeCost);
                UpgradeLevel++;
                GameUI.UpdatePlayerInfo();
                GameUI.ShowNotification($"{_owner.Name} upgraded {Name} to level {UpgradeLevel}");
            }
            else if (_owner.Money < UpgradeCost)
            {
                GameUI.ShowNotification($"{_owner.Name} doesn't have enough money to upgrade {Name}.");
            }
            else
            {
                GameUI.ShowNotification($"{Name} is already at max level.");
            }
        }

        public void PayRent(GameParticipant player)
        {
            if (IsOwned && _owner != player)
            {
                int rentToPay = Rent + (UpgradeLevel * 10); // Example rent formula
                player.ModifyMoney(-rentToPay);
                _owner.ModifyMoney(rentToPay);
                GameUI.ShowNotification($"{player.Name} paid {rentToPay} rent to {_owner.Name} for {Name}.");
            }
        }
    }
}
