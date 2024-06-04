using Players;

namespace Fields
{
    public class NeutralField : Field
    {
        public string FieldName { get; }
        
        public NeutralField(string name)
        {
            FieldName = name;
        }

        public override void OnPlayerLanded(Player player) { }
    }
}
