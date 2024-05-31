using UnityEngine;
using Players;

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
            Debug.Log($"{player.Name} landed on a bonus field and received {_moneyAmount} money.");
        }
    }
}
