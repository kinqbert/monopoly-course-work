using System.Collections;
using Game;
using Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CasinoUIManager : MonoBehaviour
    {
        public static CasinoUIManager Instance;
        
        public Dice dice;

        public GameObject casinoPanel;
        public TMP_InputField betAmountInput;
        public Button betOddButton;
        public Button betEvenButton;
        public Button betDoubleButton;
        public Button leaveCasinoButton;
        public TextMeshProUGUI resultText;
        public bool IsActive => casinoPanel.activeSelf;
        
        private bool _betsPlaced;
        private Player _currentPlayer;
        private string _currentBetType;
        private int _currentBetAmount;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            casinoPanel.SetActive(false);
            betOddButton.onClick.AddListener(() => StartCoroutine(PlaceBet("odd")));
            betEvenButton.onClick.AddListener(() => StartCoroutine(PlaceBet("even")));
            betDoubleButton.onClick.AddListener(() => StartCoroutine(PlaceBet("double")));
            leaveCasinoButton.onClick.AddListener(LeaveCasino);
        }

        public void OpenCasino(Player player)
        {
            _currentPlayer = player;
            
            casinoPanel.SetActive(true);
            resultText.text = "";
        }

        private IEnumerator PlaceBet(string betType)
        {
            resultText.text = "";
            _betsPlaced = true;
            
            if (int.TryParse(betAmountInput.text, out int betAmount) && betAmount > 0 && betAmount <= _currentPlayer.Money)
            {
                _currentBetType = betType;
                _currentBetAmount = betAmount;
                _currentPlayer.ModifyMoney(-betAmount);
                
                GameUI.UpdatePlayerInfo();

                // roll the dice and wait for the animation to complete
                GameManager.Instance.RollDiceForCasino();
                yield return new WaitUntil(() => dice.GetDoneRolling());
                
                HandleCasinoRollComplete();
            }
            else
            {
                resultText.text = "Invalid bet amount.";
            }
        }

        public void PlaceBet(Player player, string betType, int betAmount)
        {
            _currentPlayer = player;
            _betsPlaced = true;

            if (betAmount > 0 && betAmount <= _currentPlayer.Money)
            {
                _currentBetType = betType;
                _currentBetAmount = betAmount;
                _currentPlayer.ModifyMoney(-betAmount);

                GameUI.UpdatePlayerInfo();

                // Roll the dice and wait for the animation to complete
                GameManager.Instance.RollDiceForCasino();

                if (!(player is AiPlayer))
                {
                    HandleCasinoRollComplete();
                    StartCoroutine(WaitForDiceRoll());
                }
            }
        }

        private IEnumerator WaitForDiceRoll()
        {
            yield return new WaitUntil(() => dice.GetDoneRolling());
            HandleCasinoRollComplete();
        }

        public void HandleCasinoRollComplete()
        {
            // had to add this check to prevent multiple calls because of some weird Unity bug
            if (!_betsPlaced) return;
            
            int diceRoll1 = dice.GetCurrentValues()[0];
            int diceRoll2 = dice.GetCurrentValues()[1];
            int total = diceRoll1 + diceRoll2;
            bool isDouble = diceRoll1 == diceRoll2;

            string resultMessage = $"You rolled {diceRoll1} and {diceRoll2}. ";

            switch (_currentBetType)
            {
                case "odd":
                    if (total % 2 != 0)
                    {
                        _currentPlayer.ModifyMoney(_currentBetAmount * 2);
                        resultMessage += $"You won! Total money: {_currentPlayer.Money}";
                    }
                    else
                    {
                        resultMessage += $"You lost. Total money: {_currentPlayer.Money}";
                    }
                    break;
                case "even":
                    if (total % 2 == 0)
                    {
                        _currentPlayer.ModifyMoney(_currentBetAmount * 2);
                        resultMessage += $"You won! Total money: {_currentPlayer.Money}";
                    }
                    else
                    {
                        resultMessage += $"You lost. Total money: {_currentPlayer.Money}";
                    }
                    break;
                case "double":
                    if (isDouble)
                    {
                        _currentPlayer.ModifyMoney(_currentBetAmount * 10);
                        resultMessage += $"You won! Total money: {_currentPlayer.Money}";
                    }
                    else
                    {
                        resultMessage += $"You lost. Total money: {_currentPlayer.Money}";
                    }
                    break;
            }

            _betsPlaced = false;
            resultText.text = resultMessage;
            GameUI.UpdatePlayerInfo();
        }

        public void LeaveCasino()
        {
            casinoPanel.SetActive(false);
        }
    }
}
