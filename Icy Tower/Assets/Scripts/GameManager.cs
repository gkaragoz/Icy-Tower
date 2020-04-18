using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Action<GameState, GameState> OnGameStateChanged;

    [SerializeField]
    private LoadManager _loadManager = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private GameState _gameState = GameState.Loading;

    private bool _hasPlayerDied = false;

    public bool HasPlayerDied {
        get { return _hasPlayerDied; }
        set { _hasPlayerDied = value; }
    }

    #region Singleton

    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    private void Start() {
        SceneManager.sceneLoaded += OnSceneActivated;

        _loadManager.OnAccountLoaded += OnAccountLoaded;
        _loadManager.OnSceneReady += OnSceneReady;
        _loadManager.OnPoolLoaded += OnPoolLoaded;

        _loadManager.LoadAccount();
        _loadManager.LoadPool();
        _loadManager.LoadScene();
    }


    private void OnAccountLoaded() {
        Debug.Log("Account loaded!");
    }

    private void OnSceneReady() {
        _loadManager.OnSceneReady -= OnSceneReady;
        Debug.Log("Ready!");

        _loadManager.OpenLoadedScene();
    }

    private void OnPoolLoaded() {
        Debug.Log("Pool loaded!");
    }
    private void OnSceneActivated(Scene arg0, LoadSceneMode arg1) {
        SetGameState(GameState.MainMenu);
    }

    public GameState GetGameState() {
        return this._gameState;
    }

    public void SetGameState(GameState state) {
        GameState previousState = this._gameState;
        this._gameState = state;

        switch (state) {
            case GameState.Loading:
                break;
            case GameState.MainMenu:
                break;
            case GameState.GamePaused:
                break;
            case GameState.GameplayCountdown:
                break;
            case GameState.Gameplay:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(previousState, this._gameState);
    }

}
