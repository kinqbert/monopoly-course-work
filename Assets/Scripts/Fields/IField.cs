using Players;

namespace Fields
{
    public abstract class Field
    {
        public string Name;
        public abstract void OnPlayerLanded(GameParticipant player);
    }
}


