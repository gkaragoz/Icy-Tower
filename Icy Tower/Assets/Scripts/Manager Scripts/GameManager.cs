using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private Transform _leftMapSpawnTransform = null;
    [SerializeField]
    private Transform _rightMapSpawnTransform = null;
    [SerializeField]
    private CollectableSpawner _collectableSpawner = null;
    [SerializeField]
    private PlayerController _playerController = null;
    [SerializeField]
    private PlayerStats _playerStats = null;

    [SerializeField]
    private int _countDownTime = 3;

    public Action<PlayerStats> OnPlayerStatsChanged;
    public Action<GameState> OnGameStateChanged;

    [Header("Debug")]
    [Utils.ReadOnly]
    [SerializeField]
    private float _gravityScale = 1.0f;
    [SerializeField]
    [Utils.ReadOnly]
    private GameState _gameState = GameState.MainMenu;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isGamePaused = false;
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

    public enum GameState {
        MainMenu,
        GamePaused,
        NewGame,
        RestartGame,
        GameplayCountdown,
        Gameplay,
        GameOver
    }

    public enum GoldTypes {
        GanoverGold,
        TriangleGold,
        LineGold,
        DiagonalGold
    }

    public GameState GameStateEnum {
        get {
            return _gameState;
        }
        private set {
            _gameState = value;
            Debug.Log(">>GAME STATE HAS BEEN CHANGED: " + _gameState.ToString());
            OnGameStateChanged?.Invoke(_gameState);
        }
    }

    public Transform LeftMapSpawnTransform { get { return _leftMapSpawnTransform; } }

    public Transform RightMapSpawnTransform { get { return _rightMapSpawnTransform; } }

    public int GetCountDownCount { get { return _countDownTime; } }

    private void InitializeNewGame() {
        if (_isGamePaused) {
            Unpause();
        }

        if (!_hasGameObjectsInitialized) {
            ObjectPooler.instance.InitializePool("Platform");
            ObjectPooler.instance.InitializePool("Wall");
            foreach (string goldTypes in (string[])Enum.GetNames(typeof(GoldTypes))) {
                ObjectPooler.instance.InitializePool(goldTypes);
            }

            _hasGameObjectsInitialized = true;
        }
    }

    private IEnumerator IStartGameCountdown() {
        GameStateEnum = GameState.GameplayCountdown;

        yield return new WaitForSeconds(_countDownTime);

        StartGame();
    }

    private void StartGame() {
        SpawnManager.instance.SpawnAll();
        _collectableSpawner.StartGoldSpawns();

        GameStateEnum = GameState.Gameplay;
    }

    private void Pause() {
        Time.timeScale = 0;

        _isGamePaused = true;

        GameStateEnum = GameState.GamePaused;
    }

    private void Unpause() {
        Time.timeScale = 1;

        _isGamePaused = false;

        GameStateEnum = GameState.Gameplay;
    }

    public void OnClick_NewGame() {
        InitializeNewGame();
        
        GameStateEnum = GameState.NewGame;

        StartCoroutine(IStartGameCountdown());
    }

    public void OnClick_PauseUnpauseGame() {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused) {
            Pause();
        } else {
            Unpause();
        }
    }

    public void OnClick_RestartGame() {
        OnClick_NewGame();

        GameStateEnum = GameState.RestartGame;
    }

    public void AddGoldToPlayer() {
        _playerController.AddGold();

        OnPlayerStatsChanged?.Invoke(_playerController.PlayerStats);
    }

    public void SetScore() {
        _playerController.SetScore();

        OnPlayerStatsChanged?.Invoke(_playerController.PlayerStats);
    }
    public float GetGravityScale() {
        return _gravityScale;
    }

    public void SetGravityScale(float value) {
        _gravityScale = value;
    }
}
