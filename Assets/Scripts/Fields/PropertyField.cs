using UI;
using Players;
using Properties;

namespace Fields
{
    public class PropertyField : Field
    {
        public string Name;
        public Property Property { get; }
        
        public PropertyField(string name, int cost)
        {
            Name = name;
            Property = new Property(name, cost);
        }
        
        public PropertyField(Property property)
        {
            Name = property.Name;
            Property = property;
        }
        
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
    }
}