using UI;
using Players;

namespace Fields
{
    public class JailField : Field
    {
        private const int JailTurns = 3;
        public string FieldName { get; }

        public JailField(string fieldName)
        {
            FieldName = fieldName;
        }

        public override void OnPlayerLanded(Player player)
        {
            player.SendToJail(JailTurns);
            GameUI.ShowNotification($"{player.Name} is sent to jail for {JailTurns} turns.");
        }
    }
}