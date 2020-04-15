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

    public void AddGold(int value) {
        PlayerStats.AddGold(value);

        SaveSystem.SavePlayer(_playerStats);
        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void AddKey(int value) {
        PlayerStats.AddKey(value);

        SaveSystem.SavePlayer(_playerStats);
        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetCurrentScore(int value) {
        PlayerStats.SetCurrentScore(value);

        SaveSystem.SavePlayer(_playerStats);
        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetHighScore(int value) {
        PlayerStats.SetHighScore(value);

        SaveSystem.SavePlayer(_playerStats);
        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetGold(int value) {
        PlayerStats.SetGold(value);

        SaveSystem.SavePlayer(_playerStats);
        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetKey(int value) {
        PlayerStats.SetKey(value);

        SaveSystem.SavePlayer(_playerStats);
        OnPlayerStatsChanged?.Invoke(PlayerStats);
    }

    public void SetGem(int value) {
        PlayerStats.SetGem(value);

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

    public int GetGold() {
        return PlayerStats.GetGold();
    }

    public int GetKey() {
        return PlayerStats.GetKey();
    }

    public int GetGem() {
        return PlayerStats.GetGem();
    }

}
