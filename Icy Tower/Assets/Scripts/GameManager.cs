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
        if(state == GameState.NewGame) {
            InitializePool();
        }else if(state == GameState.Gameplay) {
            _collectableSpawner.StartGoldSpawns();
            _collectableSpawner.StartPowerUpSpawns();
        }
    }

    private void InitializePool() {
        if (!_hasGameObjectsInitialized) {
            ObjectPooler.instance.InitializePool("Platform");
            ObjectPooler.instance.InitializePool("Wall");
            foreach (string goldTypes in (string[])Enum.GetNames(typeof(GoldHolderTypes))) {
                ObjectPooler.instance.InitializePool(goldTypes);
            }
            foreach (string collectables in (string[])Enum.GetNames(typeof(Collectables))) {
                ObjectPooler.instance.InitializePool(collectables);
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

    public void SetScore() {
        _playerController.SetScore();

        OnPlayerStatsChanged?.Invoke(_playerController.PlayerStats);
    }

}
