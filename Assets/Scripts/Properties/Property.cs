using Players;
using UI;

namespace Properties
{
    public class Property
    {
        public string Name { get; } 
        public int UpgradeLevel { get; private set; } // 1-5
        public bool IsOwned => _owner != null;
        public int Price => _initialPrice + (int)(_initialPrice * 0.25 * (UpgradeLevel - 1)); // dynamic price calculation, some crazy math here
        public int UpgradeCost => _initialPrice / 10 + UpgradeLevel * 50;
        public int Rent => _initialPrice / 10 * UpgradeLevel;

        private int _initialPrice;
        private Player _owner;

        public Property(string name, int price)
        {
            Name = name;
            UpgradeLevel = 1; // start with level 1, not 0
            
            _initialPrice = price;
            _owner = null;
        }

        public void BuyProperty(Player player)
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
            GameUI.ShowNotification($"{_owner.Name} sold {Name} for ${Price} money.");
            UpgradeLevel = 1; // Reset to level 1
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

        public void PayRent(Player player)
        {
            if (IsOwned && _owner != player)
            {
                player.ModifyMoney(-Rent);
                _owner.ModifyMoney(Rent);
                GameUI.ShowNotification($"{player.Name} paid {Rent} rent to {_owner.Name} for {Name}.");
            }
        }
        
        public void ResetProperty()
        {
            UpgradeLevel = 1;
            _owner = null;
        }
    }
}
