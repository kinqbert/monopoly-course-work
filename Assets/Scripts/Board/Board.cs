using UnityEngine;

namespace Board
{
    public class Board : MonoBehaviour
    {
        public static readonly int CellsCount = 36;
        public Tile[] Tiles; // This will be set in the Inspector

        public static Tile[] Cells { get; private set; }

        void Awake()
        {
            // Ensure Tiles array is initialized
            if (Tiles != null && Tiles.Length == CellsCount)
            {
                Cells = new Tile[CellsCount];
                for (int i = 0; i < CellsCount; i++)
                {
                    Cells[i] = Tiles[i];
                }
            }
            else
            {
                Debug.LogError("Tiles array is not properly initialized in the Inspector!");
            }
        }

        void Start()
        {
            // Any additional setup
        }

        void Update()
        {
        }

        public static Tile GetTile(int index)
        {
            return Cells[index];
        }
    }
}