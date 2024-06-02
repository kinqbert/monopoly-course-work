using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fields;
using Properties;

namespace Players
{
    public class GameParticipant : MonoBehaviour
    {
        public string Name { get; private set; }
        public int Money { get; private set; }
        public List<Property> Properties;

        private Tile _startingTile;
        public Tile CurrentTile { get; private set; }
        private int _currentTileIndex;
    
        // BOARD REFERENCE
        private Board.Board _board;
        
        // ANIMATION VARIABLES
        private Vector3 _velocity = Vector3.zero;
        private readonly float _liftTime = 0.2f;
        private readonly float _smoothTime = 0.25f;
        private readonly float _liftHeight = 1f; // height to lift the player piece
        private readonly float _offsetRadius = 0.2f; // radius for circular offset

        public bool IsInJail { get; private set; }
        public int JailTurns { get; private set; }

        public void ModifyMoney(int amount)
        {
            Money += amount;
        }

        public void AddProperty(Property property)
        {
            Properties.Add(property);
        }
        
        public void RemoveProperty(Property property)
        {
            Properties.Remove(property);
        }

        public void Move(int steps)
        {
            int finalTileIndex = (_currentTileIndex + steps) % Board.Board.CellsCount;
            Tile finalTile = _board.GetTile(finalTileIndex);

            _currentTileIndex = finalTileIndex;
            CurrentTile = finalTile;

            StartCoroutine(MoveToTile(finalTile));
        }
        
        public void SendToJail(int turns)
        {
            IsInJail = true;
            JailTurns = turns;
        }

        public void ReleaseFromJail()
        {
            IsInJail = false;
            JailTurns = 0;
        }

        public void DecrementJailTurns()
        {
            JailTurns--;
            if (JailTurns <= 0)
            {
                ReleaseFromJail();
            }
        }
        
        // method to initialize the player
        public void Initialize(string playerName)
        {
            _board = GameObject.Find("Board").GetComponent<Board.Board>();

            Name = playerName;
            Money = 1500;
            _currentTileIndex = 0;
            _startingTile = _board.GetTile(_currentTileIndex);
            CurrentTile = _startingTile;
            transform.position = ApplyCircularOffset(_startingTile);
            Properties = new List<Property>();
        }

        // animation chain to move the player piece to the target tile
        private IEnumerator MoveToTile(Tile tile)
        {
            // go up
            yield return StartCoroutine(LiftUp());

            // move to position above the target tile
            Vector3 positionAboveTarget = tile.transform.position + Vector3.up * _liftHeight;
            yield return StartCoroutine(MoveToPosition(positionAboveTarget));

            // Apply circular offset if multiple players are on the same tile
            Vector3 offsetPosition = ApplyCircularOffset(tile);
            yield return StartCoroutine(MoveToPosition(offsetPosition));

            // go down
            yield return StartCoroutine(LiftDown(offsetPosition));

            // update the current tile
        }

        // coroutine to move the player piece to the target position
        private IEnumerator MoveToPosition(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, _smoothTime);
                yield return null;
            }
            transform.position = target;
        }

        // coroutine to lift the player piece up before moving to the target position
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

        // coroutine to lift the player piece down after moving to the target position
        private IEnumerator LiftDown(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, _liftTime);
                yield return null;
            }
            transform.position = target;
        }

        // method to apply circular offset to player pieces on the same tile so they wouldn't overlap
        private Vector3 ApplyCircularOffset(Tile tile)
        {
            Vector3 offset = Vector3.zero;
            GameParticipant[] playersOnTile = FindObjectsOfType<GameParticipant>();

            int index = 0;
            foreach (GameParticipant player in playersOnTile)
            {
                if (player.CurrentTile == tile && player != this)
                {
                    index++;
                }
            }

            // oh so that's why we had to learn math in school 
            float angle = index * (2 * Mathf.PI / 4); 
            offset.x = Mathf.Cos(angle) * _offsetRadius;
            offset.z = Mathf.Sin(angle) * _offsetRadius;

            return tile.transform.position + offset;
        }
    }
}
