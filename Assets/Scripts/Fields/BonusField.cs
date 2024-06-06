using UI;
using Players;

namespace Fields
{
    public class BonusField : Field
    {
        public string FieldName { get; }
        private int _bonusAmount; // positive for bonus, negative for penalty
        
        public BonusField(string fieldName, int bonus)
        {
            FieldName = fieldName;
            _bonusAmount = bonus;
        }

        public override void OnPlayerLanded(Player player)
        {
            player.ModifyMoney(_bonusAmount);
            
            if (_bonusAmount > 0)
                GameUI.ShowNotification($"{player.Name} was promoted and got ${_bonusAmount}");
            else
                GameUI.ShowNotification($"{player.Name} got taxed and lost -${-_bonusAmount}");
        }
    }
}
