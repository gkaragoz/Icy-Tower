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
        if (newGameState == GameState.GameOver) {
            SaveSystem.SavePlayer(_playerStats);
            OnPlayerStatsChanged?.Invoke(PlayerStats);
        }
        if (newGameState == GameState.MainMenu) {
            ResetCurrentScore();
            ResetCombo();

            SaveSystem.SavePlayer(_playerStats);
            OnPlayerStatsChanged?.Invoke(PlayerStats);
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
    public void Init(bool isOfflineMode) {
        Debug.Log("Account will initializing in offline mode...");

        PlayerStats_SO readedSO = SaveSystem.LoadPlayer();

        // First time save or local file is broken
        if (readedSO == null) {
            // Just please take local unity market items and assert into my player.
            _playerStats.MarketItems = MarketManager.instance.MarketItems;

            // Save my new stats and market and CREATE my local file for the first time.
            SaveSystem.SavePlayer(_playerStats);
        } 
        // I have a local file.
        else {
            // Please load local file to my runtime player.
            _playerStats = readedSO;

            // Is there any fetched data from cloud?
            if (MarketManager.instance.IsFetchedByOnline) {
                // Set cloud market data to my runtime player.
                _playerStats.MarketItems = MarketManager.instance.MarketItems;
            } else {
                // Probably I'm in offline mode or market has been broken. So set my market items via my local file.
                MarketManager.instance.Init(_playerStats.MarketItems);
            }
        }

        // If I'm in online mode, always sync and override the cloud.
        if (isOfflineMode == false) {
            CloudSaver.Sync(_playerStats);
        }

        OnPlayerStatsChanged?.Invoke(PlayerStats);

        Debug.Log("User account has been initialized.");
    }

    public void Save() {
        SaveSystem.SavePlayer(_playerStats);
        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void AddCloth(ClothType clothType, string id, bool save = false) {
        PlayerStats.AddClothItem(clothType, id);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void AddVirtualCurrency(int amount, VirtualCurrency vc) {
        switch (vc) {
            case VirtualCurrency.Gold:
                AddGold(amount, true);
                break;
            case VirtualCurrency.Gem:
                AddGem(amount, true);
                break;
            case VirtualCurrency.Key:
                AddKey(amount, true);
                break;
            default:
                break;
        }
    }

    public void AddCombo(bool save = false) {
        PlayerStats.AddCombo();

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void AddGold(int value, bool save = false) {
        PlayerStats.AddGold(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void AddGem(int value, bool save = false) {
        PlayerStats.AddGem(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void AddKey(int value, bool save = false) {
        PlayerStats.AddKey(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void DecreaseVirtualCurrency(int amount, VirtualCurrency vc) {
        switch (vc) {
            case VirtualCurrency.Gold:
                DecreaseGold(amount, true);
                break;
            case VirtualCurrency.Gem:
                DecreaseGem(amount, true);
                break;
            case VirtualCurrency.Key:
                DecreaseKey(amount, true);
                break;
            default:
                break;
        }
    }

    private void DecreaseGold(int value, bool save = false) {
        PlayerStats.DecreaseGold(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void DecreaseGem(int value, bool save = false) {
        PlayerStats.DecreaseGem(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void DecreaseKey(int value, bool save = false) {
        PlayerStats.DecreaseKey(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void ResetCurrentScore() {
        SetCurrentScore(0, true);
    }

    private void ResetCombo() {
        SetCombo(0, true);
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

    public void SetCombo(int value, bool save = false) {
        PlayerStats.SetCombo(value);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
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
            case VirtualCurrency.Key:
                return PlayerStats.GetKey();
            default:
                return 0;
        }
    }

    public int GetKey() {
        return PlayerStats.GetKey();
    }

    public int GetCurrentCombos() {
        return PlayerStats.GetCombo();
    }

    public string GetClothItems(ClothType clothType) {
        switch (clothType) {
            case ClothType.Head:
                return PlayerStats.GetHeadGroup();
            case ClothType.Body:
                return PlayerStats.GetBodyGroup();
            case ClothType.Shoe:
                return PlayerStats.GetShoesGroup();
            default:
                return string.Empty;
        }
    }

}
