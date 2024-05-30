using UnityEngine;

namespace Players
{
    public class GameParticipant : MonoBehaviour
    {
        private Tile _startingTile;
        private Tile _currentTile;
        private int _currentTileIndex;
        public string Name { get; private set; }
    
        private Vector3 _targetPosition;
        private Vector3 _velocity = Vector3.zero;
        private float _smoothTime = 0.5f;
        
        // Start is called before the first frame update
        void Start()
        {
        
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
            Name = name;
            _currentTileIndex = 0;
            _startingTile = Board.Board.GetTile(_currentTileIndex);
            _currentTile = _startingTile;
            transform.position = _startingTile.transform.position;
            _targetPosition = transform.position;
        }
    
        public void Move(int steps)
        {
            int finalTileIndex = (_currentTileIndex + steps) % Board.Board.CellsCount;

            _currentTileIndex = finalTileIndex;
            _currentTile = Board.Board.GetTile(_currentTileIndex);
            
            SetNewTargetPosition(_currentTile.transform.position);
        }
        
        private void SetNewTargetPosition(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
            _velocity = Vector3.zero;
        }
    }
}