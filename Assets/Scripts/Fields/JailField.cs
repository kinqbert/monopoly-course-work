using Players;
using UI;

namespace Fields
{
    public class JailField : Field
    {
        private const int JailTurns = 3;
        public string Name { get; }

        public JailField(string name)
        {
            Name = name;
        }

        public override void OnPlayerLanded(GameParticipant player)
        {
            player.SendToJail(JailTurns);
            GameUI.ShowNotification($"{player.Name} is sent to jail for {JailTurns} turns.");
        }
    }
}