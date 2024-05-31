using System.Collections.Generic;
using Fields;
using UnityEngine;
using UnityEngine.Serialization;

namespace Board
{
    public class Board : MonoBehaviour
    {
        public static readonly int CellsCount = 36;
        
        [FormerlySerializedAs("Tiles")] public List<Tile> tiles; // This will be set in the Inspector
        private List<Field> _fields; // Array to store all fields

        void Awake()
        {
            _fields = new List<Field>();
            
            /////////////////////////////////////////////
            
            _fields.Add(new NeutralField("Start"));
            
            /////////////////////////////////////////////
            
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            
            /////////////////////////////////////////////
            
            _fields.Add(new NeutralField("Corner"));
            
            /////////////////////////////////////////////
            
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            
            /////////////////////////////////////////////
            
            _fields.Add(new NeutralField("Corner"));
            
            /////////////////////////////////////////////
            
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            
            /////////////////////////////////////////////
            
            _fields.Add(new NeutralField("Corner"));
            
            /////////////////////////////////////////////
            
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            
            /////////////////////////////////////////////
            
            if (_fields.Count != CellsCount)
            {
                Debug.LogError("Fields count is not equal to CellsCount");
            }
            
            /////////////////////////////////////////////
            
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Field = _fields[i];
            }
        }

        public Tile GetTile(int index)
        {
            return tiles[index];
        }
        
        public Field GetField(int index)
        {
            return _fields[index];
        }
    }
}


