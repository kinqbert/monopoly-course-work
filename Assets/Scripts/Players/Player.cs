using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Properties;
using Fields;
using Game;
using UI;

namespace Players
{
    public class Player : MonoBehaviour
    {
        public string Name { get; private set; }
        public int Money { get; private set; }
        public List<Property> Properties;
        private Tile _startingTile;
        protected Tile CurrentTile { get; private set; }
        private int _currentTileIndex;
        private Board.Board _board;
        
        private Vector3 _velocity = Vector3.zero;
        private const float LiftTime = 0.2f;
        private const float SmoothTime = 0.25f;
        private const float LiftHeight = 1f;
        private const float OffsetRadius = 0.2f;
        
        public bool IsInJail { get; private set; }
        protected int JailTurns { get; private set; }
        
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

        protected void Move(int steps)
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

        protected void ReleaseFromJail()
        {
            IsInJail = false;
            JailTurns = 0;
        }

        protected void DecrementJailTurns()
        {
            JailTurns--;
            if (JailTurns <= 0)
            {
                ReleaseFromJail();
            }
        }
        
        public virtual void HandleOnDiceCompleted()
        {
            if (IsInJail)
            {
                HandleJail();
            }
            else
            {
                HandleTurn();
            }
        }
        
        private void HandleTurn()
        {
            Dice dice = GameManager.Instance.dice; 
            
            Move(dice.GetTotal());
            CurrentTile.Field.OnPlayerLanded(this);
        }
        
        protected void HandleJail()
        {
            if (JailTurns > 0)
            {
                if (GameManager.Instance.dice.IsDouble())
                {
                    ReleaseFromJail();
                    GameUI.ShowNotification($"{Name} rolled a double and was released from jail.");
                }
                else
                {
                    DecrementJailTurns();
                    if (JailTurns == 0)
                    {
                        ReleaseFromJail();
                        GameUI.ShowNotification($"{Name} served the jail time and will be released next turn.");
                    }
                    else
                    {
                        GameUI.ShowNotification($"{Name} is still in jail for {JailTurns} more turn(s).");
                    }
                }
            }
        }
        
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

        // ANIMATION FUNCTIONS
        private IEnumerator MoveToTile(Tile tile)
        {
            // go up
            yield return StartCoroutine(LiftUp());

            // move to position above the target tile
            Vector3 positionAboveTarget = tile.transform.position + Vector3.up * LiftHeight;
            yield return StartCoroutine(MoveToPosition(positionAboveTarget));

            // apply circular offset so multiple players on the same tile won't overlap
            Vector3 offsetPosition = ApplyCircularOffset(tile);
            yield return StartCoroutine(MoveToPosition(offsetPosition));

            // go down
            yield return StartCoroutine(LiftDown(offsetPosition));

            // ensure the player piece is exactly at the final position
            transform.position = offsetPosition;
        }

        private IEnumerator MoveToPosition(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, SmoothTime);
                yield return null;
            }
            transform.position = target;
        }

        private IEnumerator LiftUp()
        {
            Vector3 liftTarget = transform.position + Vector3.up * LiftHeight;
            while (Vector3.Distance(transform.position, liftTarget) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, liftTarget, ref _velocity, LiftTime);
                yield return null;
            }
            transform.position = liftTarget;
        }

        private IEnumerator LiftDown(Vector3 target)
        {
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, LiftTime);
                yield return null;
            }
            transform.position = target;
        }

        private Vector3 ApplyCircularOffset(Tile tile)
        {
            Vector3 offset = Vector3.zero;
            Player[] players = FindObjectsOfType<Player>();
            int index = 0;
            foreach (Player player in players)
            {
                if (player.CurrentTile == tile && player != this)
                {
                    index++;
                }
            }
            float angle = index * (2 * Mathf.PI / 4);
            offset.x = Mathf.Cos(angle) * OffsetRadius;
            offset.z = Mathf.Sin(angle) * OffsetRadius;
            return tile.transform.position + offset;
        }

        public void HandleBankruptcy()
        {
            if (Money < 0 && Properties.Count == 0)
            {
                GameUI.ShowNotification($"{Name} is bankrupt! Removing from the game.");
                GameManager.Instance.RemovePlayer(this);
            }
        }
    }
}

