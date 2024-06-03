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
            _fields.Add(new PropertyField("Night City", 100));
            _fields.Add(new BonusField("Bonus", 50));
            _fields.Add(new PropertyField("Raccoon City", 120));
            _fields.Add(new PropertyField("Silent Hill", 140));
            _fields.Add(new PropertyField("King's Landing", 160));
            _fields.Add(new BonusField("Bonus", -50));
            _fields.Add(new PropertyField("Gotham City", 180));
            _fields.Add(new PropertyField("Araxis", 200));

            // Corner
            _fields.Add(new CasinoField("Casino"));
            // _fields.Add(new JailField("Jail"));

            // Second side: 6 cities, 2 bonus fields
            _fields.Add(new PropertyField("Albuquerque", 220));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new PropertyField("Whiterun", 240));
            _fields.Add(new PropertyField("Velen", 260));
            _fields.Add(new PropertyField("Walls of Paradis", 280));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new PropertyField("Los Santos", 300));
            _fields.Add(new PropertyField("San Andreas", 320));

            // Corner
            _fields.Add(new CasinoField("Casino"));

            // Third side: 6 cities, 2 bonus fields
            _fields.Add(new PropertyField("Blackwater", 340));
            _fields.Add(new BonusField("Bonus", 150));
            _fields.Add(new PropertyField("Liberty City", 360));
            _fields.Add(new PropertyField("Dunwall", 380));
            _fields.Add(new PropertyField("Novigrad", 400));
            _fields.Add(new BonusField("Bonus", -150));
            _fields.Add(new PropertyField("Dimetrescu's Castle", 420));
            _fields.Add(new PropertyField("Vice City", 440));

            // Corner
            _fields.Add(new TaxField("Tax Field"));

            // Fourth side: 6 cities, 2 bonus fields
            _fields.Add(new PropertyField("Baldur's Gate", 460));
            _fields.Add(new BonusField("Bonus", 200));
            _fields.Add(new PropertyField("New Vegas", 480));
            _fields.Add(new PropertyField("Black Mesa", 500));
            _fields.Add(new PropertyField("Anor Londo", 520));
            _fields.Add(new BonusField("Bonus", -200));
            _fields.Add(new PropertyField("Erangel", 540)); 
            _fields.Add(new PropertyField("Saint Denis", 560)); 
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
