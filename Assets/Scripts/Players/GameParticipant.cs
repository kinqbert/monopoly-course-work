using UnityEngine;
using Fields;

namespace Players
{
    public class GameParticipant : MonoBehaviour
    {
        public string Name { get; private set; }
        public int Money { get; private set; }


        private Tile _startingTile;
        public Tile CurrentTile { get; private set; }
        private int _currentTileIndex;
    
        private Board.Board _board;
        
        private Vector3 _targetPosition;
        private Vector3 _velocity = Vector3.zero;
        private readonly float _smoothTime = 0.5f;
        
        public int getPlayerMoney()
        {
            return Money;
        }
        
        public void ModifyMoney(int amount)
        {
            Money += amount;
        }
        
        public void Move(int steps)
        {
            int finalTileIndex = (_currentTileIndex + steps) % Board.Board.CellsCount;

            _currentTileIndex = finalTileIndex;
            CurrentTile = _board.GetTile(_currentTileIndex);
            
            SetNewTargetPosition(CurrentTile.transform.position);
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(transform.position, _targetPosition) > 0)
            {
                transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, _smoothTime);
            }
        }
    
        public void Initialize(string name)
        {
            _board = GameObject.Find("Board").GetComponent<Board.Board>();
                
            Name = name;
            _currentTileIndex = 0;
            _startingTile = _board.GetTile(_currentTileIndex);
            CurrentTile = _startingTile;
            transform.position = _startingTile.transform.position;
            _targetPosition = transform.position;
        }
        
        private void SetNewTargetPosition(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _velocity = Vector3.zero;
        }
    }
}