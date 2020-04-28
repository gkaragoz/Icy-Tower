using UnityEngine;

public class IngameStatistics : MonoBehaviour {

    [SerializeField]
    private PauseOrGameoverStats[] _pauseOrGameoverStats = null;
    public int gameInsideGold;
    private void Start() {
        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
        UpdateUI();
        gameInsideGold = Account.instance.GetCurrencyAmount(VirtualCurrency.Gold);
    }

    private void OnGameStateChanged(GameState arg1, GameState currentState) {
        if (currentState == GameState.GameOver) {
            UpdateUI();
            gameInsideGold = 0;
        }
        if (currentState == GameState.Gameplay)
        {
            UpdateUI();
            gameInsideGold = Account.instance.GetCurrencyAmount(VirtualCurrency.Gold);
        }
    }

    public void UpdateUI() {
        for (int ii = 0; ii < _pauseOrGameoverStats.Length; ii++) {
            string currentScore = Account.instance.GetCurrentScore().ToString();
            string combo = Account.instance.GetCurrentCombos().ToString();
            string gold = (Account.instance.GetCurrencyAmount(VirtualCurrency.Gold)-gameInsideGold).ToString();
            _pauseOrGameoverStats[ii].SetText(currentScore, combo, gold);
        }
    }

}
