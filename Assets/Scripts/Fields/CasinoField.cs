using Players;
using UI;

namespace Fields
{
    public class CasinoField : Field
    {
        public string Name { get; }

        public CasinoField(string name)
        {
            Name = name;
        }

        public override void OnPlayerLanded(GameParticipant player)
        {
            CasinoUIManager.Instance.OpenCasino(player);
        }
    }
}