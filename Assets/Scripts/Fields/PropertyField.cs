using UnityEngine;
using Players;

namespace Fields
{
    public class PropertyField : Field
    {
        public PropertyField(string name, int cost, int rent)
        {
            
        }

        public string Name;
        public bool IsOwned;

        public override void OnPlayerLanded(GameParticipant player)
        {
            // if (!IsOwned && player.Money >= cost)
            // {
            //     player.ModifyMoney(-cost);
            //     owner = player;
            //     Debug.Log($"{player.Name} bought {propertyName} for {cost} money.");
            // }
            // else if (IsOwned && owner != player)
            // {
            //     player.ModifyMoney(-rent);
            //     owner.ModifyMoney(rent);
            //     Debug.Log($"{player.Name} paid {rent} rent to {owner.Name} for {propertyName}.");
            // }
        }
    }
}

