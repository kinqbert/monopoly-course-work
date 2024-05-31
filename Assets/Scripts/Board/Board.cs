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
        private List<Field> _fields; // Array to store all fields

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

            _fields.Add(new NeutralField("Start"));

            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));

            _fields.Add(new NeutralField("Corner"));

            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));

            _fields.Add(new NeutralField("Corner"));

            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));

            _fields.Add(new NeutralField("Corner"));

            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
            _fields.Add(new BonusField("Bonus", 100));
            _fields.Add(new BonusField("Bonus", -100));
        }

        private void BindFieldsToTiles()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Field = _fields[i];
                Debug.Log($"Tile {i} assigned field: {_fields[i].GetType().Name}, {_fields[i].FieldName}");
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
