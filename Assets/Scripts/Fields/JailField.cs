using Game;
using Players;
using UI;
using UnityEngine;

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
            Debug.Log($"{player.Name} is sent to jail for {JailTurns} turns.");
        }

        // public override void OnPlayerStartTurn(GameParticipant player)
        // {
        //     if (player.IsInJail)
        //     {
        //         bool rolledDouble = GameManager.Instance.RollDiceForJail();
        //         if (rolledDouble)
        //         {
        //             player.ReleaseFromJail();
        //             Debug.Log($"{player.Name} rolled a double and will be released from jail next turn.");
        //         }
        //         else
        //         {
        //             player.DecrementJailTurns();
        //             Debug.Log($"{player.Name} is still in jail for {player.JailTurns} more turns.");
        //         }
        //     }
        // }
    }
}
