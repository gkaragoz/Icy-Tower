using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private CollectableSpawner _collectableSpawner = null;
    [SerializeField]
    private PlayerController _playerController = null;
    [SerializeField]
    private PlayerStats _playerStats = null;

    public Action<PlayerStats> OnPlayerStatsChanged;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _hasGameObjectsInitialized = false;

    #region Singleton

    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        _playerStats.LoadData();
    }

    #endregion

    private void Start() {
        LevelManager.instance.OnGameStateChanged += InitializeNewGame;
    }

    private void InitializeNewGame(GameState state) {
        if (state == GameState.NewGame) {
            InitializePool();
        } else if (state == GameState.Gameplay) {
            _collectableSpawner.StartGoldSpawns();
            _collectableSpawner.StartPowerUpSpawns();
        }
    }

    private void InitializePool() {
        if (!_hasGameObjectsInitialized) {
            ObjectPooler.instance.InitializePool("Platform");
            ObjectPooler.instance.InitializePool("Wall");

            foreach (string goldType in (string[])Enum.GetNames(typeof(GoldHolderTypes))) {
                ObjectPooler.instance.InitializePool(goldType, true);
            }

            foreach (string vfxType in (string[])Enum.GetNames(typeof(VFXTypes))) {
                ObjectPooler.instance.InitializePool(vfxType);
            }

            foreach (string collectable in (string[])Enum.GetNames(typeof(Collectables))) {
                ObjectPooler.instance.InitializePool(collectable);
            }

            foreach (string soundfxtype in (string[])Enum.GetNames(typeof(SoundFXTypes))) {
                ObjectPooler.instance.InitializePool(soundfxtype);
            }

            _hasGameObjectsInitialized = true;
        }
    }


    public void AddGoldToPlayer(int value) {
        _playerController.AddGold(value);

        OnPlayerStatsChanged?.Invoke(_playerController.PlayerStats);
    }

    public void AddKeyToPlayer(int value) {
        _playerController.AddKey(value);

        OnPlayerStatsChanged?.Invoke(_playerController.PlayerStats);
    }

    public void SetScore(int value) {
        _playerController.SetScore(value);

        OnPlayerStatsChanged?.Invoke(_playerController.PlayerStats);
    }

}
