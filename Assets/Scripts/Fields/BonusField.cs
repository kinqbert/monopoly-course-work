using Players;
using UI;

namespace Fields
{
    public class BonusField : Field
    {
        public new string FieldName { get; }
        private int _moneyAmount; // positive for bonus, negative for penalty
        
        public BonusField(string fieldName, int money)
        {
            FieldName = fieldName;
            _moneyAmount = money;
        }

        public override void OnPlayerLanded(GameParticipant player)
        {
            player.ModifyMoney(_moneyAmount);
            GameUI.ShowNotification($"{player.Name} landed on {FieldName} and got ${_moneyAmount}");
        }
    }
}
