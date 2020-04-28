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
        _loadManager.OnGPGSAccountInitializationBegin += OnGPGSAccountInitializationBegin;
        _loadManager.OnGPGSAccountInitializationSuccess += OnGPGSAccountInitializationSuccess;
        _loadManager.OnGPGSAccountInitiailzationFailed += OnGPGSAccountInitiailzationFailed;

        _loadManager.OnPlayFabAccountInitializationBegin += OnPlayFabAccountInitializationBegin;
        _loadManager.OnPlayFabAccountInitializationSuccess += OnPlayFabAccountInitializationSuccess;
        _loadManager.OnPlayFabAccountInitiailzationFailed += OnPlayFabAccountInitiailzationFailed;

        _loadManager.OnAccountLoaded += OnAccountLoaded;
        _loadManager.OnSceneReady += OnSceneReady;
        _loadManager.OnPoolLoaded += OnPoolLoaded;

        _loadManager.LoadScene();
        _loadManager.LoadAccount();
        _loadManager.LoadPool();

        _loadManager.InitAuth();
    }

    private void OnGPGSAccountInitializationBegin() {
        Debug.Log("OnGPGSAccountInitializationBegin!");
    }

    private void OnGPGSAccountInitializationSuccess() {
        Debug.Log("OnGPGSAccountInitializationSuccess!");
    }

    private void OnGPGSAccountInitiailzationFailed() {
        Debug.Log("OnGPGSAccountInitiailzationFailed!");
    }

    private void OnPlayFabAccountInitializationBegin() {
        Debug.Log("OnPlayFabAccountInitializationBegin!");
    }

    private void OnPlayFabAccountInitializationSuccess() {
        Debug.Log("OnPlayFabAccountInitializationSuccess!");
    }

    private void OnPlayFabAccountInitiailzationFailed() {
        Debug.Log("OnPlayFabAccountInitiailzationFailed!");
    }

    private void OnAccountLoaded() {
        _loadManager.OnAccountLoaded -= OnAccountLoaded;
        Debug.Log("OnAccountLoaded!");
    }

    private void OnSceneReady() {
        _loadManager.OnSceneReady -= OnSceneReady;
        Debug.Log("OnSceneReady!");

        bool HasAuthInitialiationCompleted = false;
        if (HasAuthInitialiationCompleted) {
            _loadManager.OpenLoadedScene();

            SetGameState(GameState.MainMenu);

            UIManager.instance.OnUISceneChanged += OnUISceneChanged;
        } else {
            // TODO POPUP
            Debug.Log("Auth process failed. Not gonna load scene.");
        }
    }

    private void OnPoolLoaded() {
        _loadManager.OnPoolLoaded -= OnPoolLoaded;
        Debug.Log("OnPoolLoaded!");
    }

    private void OnUISceneChanged(UIPanels newPanel) {
        if (newPanel == UIPanels.PnlMainMenu) {
            SetGameState(GameState.MainMenu);
            ///// 
            /// I found just this state to make my spawn floor 0
            /// 
            CollectableSpawner.instance.ResetGoldSpawnFloor();
            SpawnManager.instance.ResetWalls();

        } else if (newPanel == UIPanels.PnlWardrobe) {
            SetGameState(GameState.Wardrobe);
        }
    }

    public GameState GetGameState() {
        return this._gameState;
    }

    public void SetGameState(GameState state) {
        GameState previousState = this._gameState;
        this._gameState = state;

        if (previousState == state) {
            Debug.Log("States are same.");
            return;
        }

        Debug.Log("Previous state: " + previousState);
        Debug.Log("Next state: " + state);

        switch (state) {
            case GameState.Loading:
                break;
            case GameState.MainMenu:
                break;
            case GameState.GamePaused:
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
