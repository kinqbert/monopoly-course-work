using System.Collections;
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
        
        private Vector3 _velocity = Vector3.zero;
        private readonly float _liftTime = 0.2f;
        private readonly float _smoothTime = 0.25f;
        private readonly float _liftHeight = 1f; // height to lift the player piece
        
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
            StartCoroutine(MoveToTile(steps));
        }
    
        public void Initialize(string name)
        {
            _board = GameObject.Find("Board").GetComponent<Board.Board>();
                
            Name = name;
            _currentTileIndex = 0;
            _startingTile = _board.GetTile(_currentTileIndex);
            CurrentTile = _startingTile;
            transform.position = _startingTile.transform.position;
        }
        
        private IEnumerator MoveToTile(int steps)
        {
            int finalTileIndex = (_currentTileIndex + steps) % Board.Board.CellsCount;
            Tile finalTile = _board.GetTile(finalTileIndex);

            // go up
            yield return StartCoroutine(LiftUp());

            // move to position above the target tile
            Vector3 positionAboveTarget = finalTile.transform.position + Vector3.up * _liftHeight;
            yield return StartCoroutine(MoveToPosition(positionAboveTarget));

            // go down
            yield return StartCoroutine(LiftDown(finalTile.transform.position));

            // update the current tile
            _currentTileIndex = finalTileIndex;
            CurrentTile = finalTile;
            Debug.Log($"NAME: {Name}. CURRENT TILE: {CurrentTile.name}. STEPS: {steps}.");
        }
        
        // ANIMATION COROUTINES

        private IEnumerator MoveToPosition(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, _smoothTime);
                yield return null;
            }
            transform.position = target;
        }

        private IEnumerator LiftUp()
        {
            Vector3 liftTarget = transform.position + Vector3.up * _liftHeight;
            while (Vector3.Distance(transform.position, liftTarget) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, liftTarget, ref _velocity, _liftTime);
                yield return null;
            }
            transform.position = liftTarget;
        }

        private IEnumerator LiftDown(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, _liftTime);
                yield return null;
            }
            transform.position = target;
        }
    }
}
