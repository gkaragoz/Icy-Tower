using System;
using System.Collections.Generic;
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
    public PlayFabVCRewardsHandler RewardsVCRepo { get; set; }

    private void Start() {
        RewardsVCRepo = new PlayFabVCRewardsHandler();

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
    public void Init(bool hasFetchedData, Dictionary<string, int> fetchedVCData) {
        Debug.Log("Account will initializing...");

        PlayerStats_SO readedSO = SaveSystem.LoadPlayer();

        // First time saving game data or local file is broken and creating one.
        if (readedSO == null) {
            _playerStats = ScriptableObject.CreateInstance(typeof(PlayerStats_SO)) as PlayerStats_SO;
            _playerStats.MarketItems = MarketManager.instance.MarketItems;
        }
        // I have a local file. I played before.
        else {
            // Please load local file to my runtime player.
            _playerStats = readedSO;
            MarketManager.instance.InitBy(_playerStats.MarketItems);
        }

        // Is there any fetched data from cloud?
        if (hasFetchedData) {
            // Override cloud market fields to my local.
            MarketManager.instance.OverrideFetchedData();
            // Check VC rewards.
            AddVirtualCurrency(fetchedVCData);
        }

        // Save my new stats and market and CREATE or UPDATE my local file.
        Save();

        Debug.Log("User account & market has been initialized.");
    }

    public void Save() {
        SaveSystem.SavePlayer(_playerStats);
        CloudSaver.Sync(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void AddCloth(ClothType clothType, string id, bool save = false) {
        PlayerStats.AddClothItem(clothType, id);

        if (save)
            SaveSystem.SavePlayer(_playerStats);

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void AddVirtualCurrency(int amount, VirtualCurrency vc, bool save = false) {
        switch (vc) {
            case VirtualCurrency.Gold:
                AddGold(amount, save);
                break;
            case VirtualCurrency.Gem:
                AddGem(amount, save);
                break;
            case VirtualCurrency.Key:
                AddKey(amount, save);
                break;
            default:
                break;
        }
    }

    public void AddVirtualCurrency(Dictionary<string, int> data) {
        RewardsVCRepo = new PlayFabVCRewardsHandler();

        foreach (KeyValuePair<string, int> vc in data) {
            if (vc.Value == 0) {
                continue;
            }

            Library.PlayerData.Currency.VirtualCurrency.SubtractUserVirtualCurrency(
            () => {
                RewardsVCRepo.HasRewardCollected = true;

                if (vc.Key == PlayFabCurrencyCodes.GD.ToString()) {
                    RewardsVCRepo.RewardedGold = vc.Value;
                } else if (vc.Key == PlayFabCurrencyCodes.GM.ToString()) {
                    RewardsVCRepo.RewardedGem = vc.Value;
                } else if (vc.Key == PlayFabCurrencyCodes.KY.ToString()) {
                    RewardsVCRepo.RewardedKey = vc.Value;
                }
            },
            (errorAction) => {

            },
            vc.Key,
            vc.Value);
        }
    }

    public void AddCombo(bool save = false) {
        PlayerStats.AddCombo();

        if (save)
            Save();

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void AddGold(int value, bool save = false) {
        PlayerStats.AddGold(value);

        if (save)
            Save();

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void AddGem(int value, bool save = false) {
        PlayerStats.AddGem(value);

        if (save)
            Save();

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void AddKey(int value, bool save = false) {
        PlayerStats.AddKey(value);

        if (save)
            Save();

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
            Save();

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void DecreaseGem(int value, bool save = false) {
        PlayerStats.DecreaseGem(value);

        if (save)
            Save();

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    private void DecreaseKey(int value, bool save = false) {
        PlayerStats.DecreaseKey(value);

        if (save)
            Save();

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
            Save();

        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetHighScore(int value, bool save = false) {
        PlayerStats.SetHighScore(value);

        if (save)
            Save();

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
