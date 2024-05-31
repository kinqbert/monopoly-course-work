using Players;
using UnityEngine;

namespace Fields
{
    public class NeutralField : Field
    {
        public new string FieldName { get; }
        
        public NeutralField(string name)
        {
            FieldName = name;
        }

        public override void OnPlayerLanded(GameParticipant player)
        {
            Debug.Log($"{player.Name} landed on a neutral field: {FieldName}");
        }
    }
}
