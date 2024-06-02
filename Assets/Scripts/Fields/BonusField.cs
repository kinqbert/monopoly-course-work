using Players;
using UI;

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

        public override void OnPlayerLanded(GameParticipant player)
        {
            player.ModifyMoney(_bonusAmount);
            
            if (_bonusAmount > 0)
                GameUI.ShowNotification($"{player.Name} landed on {FieldName} and got ${_bonusAmount}");
            else
                GameUI.ShowNotification($"{player.Name} landed on {FieldName} and lost -${-_bonusAmount}");
        }
    }
}
