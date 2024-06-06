using Players;

namespace Fields
{
    public class StartField : Field
    {
        public string FieldName { get; }
        
        public StartField(string name)
        {
            FieldName = name;
        }

        public override void OnPlayerLanded(Player player) { }
    }
}
