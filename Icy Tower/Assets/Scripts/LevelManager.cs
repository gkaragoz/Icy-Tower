using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour{

    [Header("Initializations")]
    [SerializeField]
    private int _countDownTime = 5;

    [SerializeField]
    [Utils.ReadOnly]
    private bool _isGamePaused = false;

    private bool _isUsingUmbrella = false;
    private bool _isUsingStickyPlumber = false;

    #region Singleton

    public static LevelManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public int CountDownTime {
        get { return _countDownTime; }
    }

    public bool IsUsingUmbrella {
        get { return _isUsingUmbrella; }
        set { _isUsingUmbrella = value; }
    }
    public bool IsUsingStickyPlumber {
        get { return _isUsingStickyPlumber; }
        set { _isUsingStickyPlumber= value; }
    }

    private void StartGame() {
        SpawnManager.instance.SpawnAll();

        GameManager.instance.SetGameState(GameState.Gameplay);
        Account.instance.SetCurrentScore(0);
    }

    // UI
    private void Pause() {
        Time.timeScale = 0;

        _isGamePaused = true;

        GameManager.instance.SetGameState(GameState.GamePaused);
    }

    // UI
    private void Unpause() {
        Time.timeScale = 1;

        _isGamePaused = false;

        GameManager.instance.SetGameState(GameState.Gameplay);
    }

    // UI
    public void OnClick_NewGame() {
        StartGame();
    }

    // UI
    public void OnClick_PauseUnpauseGame() {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused) {
            Pause();
        } else {
            Unpause();
        }
    }

}
