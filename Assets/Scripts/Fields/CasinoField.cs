using UI;
using Players;

namespace Fields
{
    public class CasinoField : Field
    {
        public string FieldName { get; }

        public CasinoField(string fieldName)
        {
            FieldName = fieldName;
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