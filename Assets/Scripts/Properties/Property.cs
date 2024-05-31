using Players;
using UnityEngine;

namespace Properties
{
    public class Property : MonoBehaviour
    {
        public string propertyName;
        public int cost;
        public int rent;
        public GameParticipant owner;

        public bool IsOwned => owner != null;

        public void BuyProperty(GameParticipant player)
        {
            if (!IsOwned && player.Money >= cost)
            {
                player.ModifyMoney(-cost);
                owner = player;
                Debug.Log($"{player.Name} bought {propertyName} for {cost} money.");
            }
        }

        public void PayRent(GameParticipant player)
        {
            if (IsOwned && owner != player)
            {
                player.ModifyMoney(-rent);
                owner.ModifyMoney(rent);
                Debug.Log($"{player.Name} paid {rent} rent to {owner.Name} for {propertyName}.");
            }
        }
    }
}
