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

        public override void OnPlayerLanded(Player player)
        {
            if (player is AiPlayer)
            {
                (player as AiPlayer).HandleCasino();
            }
            else
            {
                CasinoUIManager.Instance.OpenCasino(player);
            }
        }
    }
}