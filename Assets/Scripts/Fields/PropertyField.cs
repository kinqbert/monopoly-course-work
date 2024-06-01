using UI;
using Players;
using Properties;

namespace Fields
{
    public class PropertyField : Field
    {
        public PropertyField(string name, int cost, int rent)
        {
            Name = name;
            Property = new Property(name, cost, rent);
        }
        
        public PropertyField(Property property)
        {
            Name = property.propertyName;
            Property = property;
        }

        public string Name;
        public Property Property { get; }

        public override void OnPlayerLanded(GameParticipant player)
        {
            if (Property.IsOwned)
            {
                Property.PayRent(player);
            }
            else
            {
                ShowConfirmationWindow(player);
            }
        }

        private void ShowConfirmationWindow(GameParticipant player)
        {
            GameUI.YesNoWindow($"Do you want to buy {Property.propertyName} for {Property.cost}?", 
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
    }
}