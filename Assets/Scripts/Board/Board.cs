using System.Collections.Generic;
using Fields;
using UnityEngine;
using UnityEngine.Serialization;

namespace Board
{
    public class Board : MonoBehaviour
    {
        public static readonly int CellsCount = 36;
        
        [FormerlySerializedAs("Tiles")] public List<Tile> tiles; // this will be set in the Inspector
        private List<Field> _fields; // array to store all fields

        void Awake()
        {
            if (tiles == null || tiles.Count != CellsCount)
            {
                Debug.LogError("Tiles array is not properly initialized or does not match CellsCount.");
                return;
            }

            InitializeFields();

            if (_fields.Count != CellsCount)
            {
                Debug.LogError("Fields count does not match CellsCount.");
                return;
            }

            BindFieldsToTiles();
        }

        private void InitializeFields()
        {
            _fields = new List<Field>();

            // Starting point
            _fields.Add(new NeutralField("Start"));

            // First side: 6 cities, 2 bonus fields
            _fields.Add(new PropertyField("Night City", 100, 10));
            _fields.Add(new BonusField("Bonus", 50));
            _fields.Add(new PropertyField("Raccoon City", 120, 12));
            _fields.Add(new PropertyField("Silent Hill", 140, 14));
            _fields.Add(new PropertyField("King's Landing", 160, 16));
            _fields.Add(new BonusField("Bonus", -50));
            _fields.Add(new PropertyField("Gotham City", 180, 18));
            _fields.Add(new PropertyField("Araxis", 200, 20));

            // Corner
            _fields.Add(new JailField("Jail"));

            // Second side: 6 cities, 2 bonus fields
            _fields.Add(new PropertyField("Albuquerque", 220, 22));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new PropertyField("Whiterun", 240, 24));
            _fields.Add(new PropertyField("Velen", 260, 26));
            _fields.Add(new PropertyField("Walls of Paradis", 280, 28));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new PropertyField("Los Santos", 300, 30));
            _fields.Add(new PropertyField("San Andreas", 320, 32));

            // Corner
            _fields.Add(new NeutralField("Corner"));

            // Third side: 6 cities, 2 bonus fields
            _fields.Add(new PropertyField("Blackwater", 340, 34));
            _fields.Add(new BonusField("Bonus", 150));
            _fields.Add(new PropertyField("Liberty City", 360, 36));
            _fields.Add(new PropertyField("Dunwall", 380, 38));
            _fields.Add(new PropertyField("Novigrad", 400, 40));
            _fields.Add(new BonusField("Bonus", -150));
            _fields.Add(new PropertyField("Dimetrescu's Castle", 420, 42));
            _fields.Add(new PropertyField("Vice City", 440, 44));

            // Corner
            _fields.Add(new NeutralField("Corner"));

            // Fourth side: 6 cities, 2 bonus fields
            _fields.Add(new PropertyField("Baldur's Gate", 460, 46));
            _fields.Add(new BonusField("Bonus", 200));
            _fields.Add(new PropertyField("New Vegas", 480, 48));
            _fields.Add(new PropertyField("Black Mesa", 500, 50));
            _fields.Add(new PropertyField("Anor Londo", 520, 52));
            _fields.Add(new BonusField("Bonus", -200));
            _fields.Add(new PropertyField("Erangel", 540, 54)); 
            _fields.Add(new PropertyField("Saint Denis", 560, 56)); 
        }

        private void BindFieldsToTiles()
        {
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
