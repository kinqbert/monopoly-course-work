using UI;
using Players;

namespace Fields
{
    public class TaxField : Field
    {
        private const int PropertyTax = 60;
        private const int UpgradeTax = 10;
        public string FieldName { get; }

        public TaxField(string fieldName)
        {
            FieldName = fieldName;
        }

        // 60 dollars for each owned property and 10 dollars for each upgrade level
        public override void OnPlayerLanded(Player player)
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