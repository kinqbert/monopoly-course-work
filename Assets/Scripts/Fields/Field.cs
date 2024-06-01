using Players;

namespace Fields
{
    public abstract class Field
    {
        public string FieldName;
        public abstract void OnPlayerLanded(GameParticipant player);
    }
}


