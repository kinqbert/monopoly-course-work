using UI;
using Players;
using Properties;

namespace Fields
{
    public class PropertyField : Field
    {
        public string FieldName;
        public Property Property { get; }
        
        public PropertyField(string fieldName, int cost)
        {
            FieldName = fieldName;
            Property = new Property(fieldName, cost);
        }
        
        public PropertyField(Property property)
        {
            FieldName = property.Name;
            Property = property;
        }
        
        public override void OnPlayerLanded(Player player)
        {
            if (Property.IsOwned)
            {
                Property.PayRent(player);
            }
            else
            {
                if (player is AiPlayer aiPlayer)
                {
                    if (player.Money >= Property.Price)
                    {
                        Property.BuyProperty(player);
                    }
                }
                else
                {
                    ShowConfirmationWindow(player);
                }
            }
        }

        private void ShowConfirmationWindow(Player player)
        {
            if (player.Money >= Property.Price)
            {
                GameUI.YesNoWindow($"Do you want to buy {Property.Name} for {Property.Price}?", 
                    () => {
                        if (!Property.IsOwned)  // Ensure the property hasn't been bought in the meantime
                        {
                            Property.BuyProperty(player);
                            GameUI.UpdatePlayerInfo();
                        }
                    }, 
                    () => {
                    });
            }
            else
            {
                GameUI.ShowNotification($"{player.Name} doesn't have enough money to buy {FieldName}.");
            }
        }
    }
}