using Players;
using UI;

namespace Fields
{
    public class TaxField : Field
    {
        private const int PropertyTax = 60;
        private const int UpgradeTax = 10;
        public string Name { get; }

        public TaxField(string name)
        {
            Name = name;
        }

        public override void OnPlayerLanded(GameParticipant player)
        {
            int totalTax = 0;

            foreach (var property in player.Properties)
            {
                totalTax += PropertyTax;
                totalTax += property.UpgradeLevel * UpgradeTax;
            }

            player.ModifyMoney(-totalTax);
            GameUI.ShowNotification($"{player.Name} paid ${totalTax} in taxes.");
        }
    }
}