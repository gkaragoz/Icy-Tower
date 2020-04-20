using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Account : MonoBehaviour {

    #region Singleton

    public static Account instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public Action<PlayerStats> OnPlayerStatsChanged;

    public PlayerStats PlayerStats { get; private set; }

    [SerializeField]
    private PlayerStats_SO _playerStats = null;

    private void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState previousGameState, GameState newGameState) {
        if (newGameState == GameState.MainMenu) {
            SaveSystem.SavePlayer(_playerStats);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        PlayerStats.Init(_playerStats);

        Debug.Log("Player stats has been assigned.");
    }
    /// <summary>
    /// Read local file or get data from cloud.
    /// </summary>
    public void Init() {
        PlayerStats_SO readedSO = SaveSystem.LoadPlayer();
        if (readedSO == null) {
            SaveSystem.SavePlayer(_playerStats);
        } else {
            _playerStats = readedSO;
        }

        OnPlayerStatsChanged?.Invoke(PlayerStats);

        Debug.Log("User accound has been initialized.");
    }

    public void AddGold(int value, bool save = false) {
        PlayerStats.AddGold(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void AddKey(int value, bool save = false) {
        PlayerStats.AddKey(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetCurrentScore(int value, bool save = false) {
        PlayerStats.SetCurrentScore(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetHighScore(int value, bool save = false) {
        PlayerStats.SetHighScore(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetGold(int value, bool save = false) {
        PlayerStats.SetGold(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetKey(int value, bool save = false) {
        PlayerStats.SetKey(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetGem(int value, bool save = false) {
        PlayerStats.SetGem(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public string GetName() {
        return PlayerStats.GetName();
    }

    public int GetCurrentScore() {
        return PlayerStats.GetCurrentScore();
    }

    public int GetHighScore() {
        return PlayerStats.GetHighScore();
    }

    public int GetCurrencyAmount(VirtualCurrency currencyType) {
        switch (currencyType) {
            case VirtualCurrency.Gold:
                return PlayerStats.GetGold();
            case VirtualCurrency.Gem:
                return PlayerStats.GetGem();
            default:
                return PlayerStats.GetGold();
        }
    }

    public int GetKey() {
        return PlayerStats.GetKey();
    }

}
