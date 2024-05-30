using System.Collections.Generic;

namespace Board
{
    public class Board
    {
        public static readonly int CellsCount = 38;
        private static Board _instance;
        private List<string> _cells;

        private Board()
        {
            _cells = new List<string>(40);
            InitializeBoard();
        }

        public static Board GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Board();
            }

            return _instance;
        }

        private void InitializeBoard()
        {
            // Initialize the board with field names
            for (int i = 0; i < CellsCount; i++)
            {
                _cells.Add($"Field {i}");
            }
        }

        public string GetCell(int index)
        {
            return _cells[index];
        }
    }
}
