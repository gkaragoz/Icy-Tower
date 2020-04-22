using UnityEngine;

public class IngameStatistics : MonoBehaviour {

    [SerializeField]
    private PauseOrGameoverStats[] _pauseOrGameoverStats = null;
    private void Start() {
        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
        UpdateUI();
    }

    private void OnGameStateChanged(GameState arg1, GameState currentState) {
        if (currentState == GameState.GameOver) {
            UpdateUI();
        }
    }

    public void UpdateUI() {
        for (int ii = 0; ii < _pauseOrGameoverStats.Length; ii++) {
            string currentScore = Account.instance.GetCurrentScore().ToString();
            string combo = Account.instance.GetCurrentCombos().ToString();
            string gold = Account.instance.GetCurrencyAmount(VirtualCurrency.Gold).ToString();
            _pauseOrGameoverStats[ii].SetText(currentScore, combo, gold);
        }
    }

}
