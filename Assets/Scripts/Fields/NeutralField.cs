using Players;

namespace Fields
{
    public class NeutralField : Field
    {
        public new string Name { get; }
        
        public NeutralField(string name)
        {
            Name = name;
        }

        public override void OnPlayerLanded(GameParticipant player) { }
    }
}
