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
        SceneManager.sceneLoaded += OnSceneActivated;

        _loadManager.OnPlayFabAuthenticationSuccess += OnPlayFabAuthenticationSuccess;
        _loadManager.OnPlayFabAuthenticationFailed += OnPlayFabAuthenticationFailed;
        _loadManager.OnGPGSAuthenticationSuccess += OnGPGSAuthenticationSuccess;
        _loadManager.OnGPGSAuthenticationFailed += OnGPGSAuthenticationFailed;

        _loadManager.OnAccountLoaded += OnAccountLoaded;
        _loadManager.OnSceneReady += OnSceneReady;
        _loadManager.OnPoolLoaded += OnPoolLoaded;

        if (Library.Authentication.PlayfabCustomAuth.ISGuestAccount() && Facebook.Unity.FB.IsLoggedIn == false) {
            _loadManager.AuthanticateToPlayFab();
        } else {
            _loadManager.AuthanticateToGPGS();
        }
    }

    private void OnPlayFabAuthenticationSuccess(string actionMessage) {
        Debug.Log("OnPlayFabAuthenticationSuccess: " + actionMessage);

        _loadManager.AuthanticateToGPGS();
    }

    private void OnPlayFabAuthenticationFailed(string actionMessage) {
        Debug.Log("OnPlayFabAuthenticationFailed: " + actionMessage);

        _loadManager.AuthanticateToGPGS();
    }

    private void OnGPGSAuthenticationSuccess(string actionMessage) {
        Debug.Log("OnGPGSAuthenticationSuccess: " + actionMessage);

        _loadManager.LoadAccount();
        _loadManager.LoadPool();
        _loadManager.LoadScene();
    }

    private void OnGPGSAuthenticationFailed(string actionMessage) {
        Debug.Log("OnGPGSAuthenticationFailed: " + actionMessage);

        _loadManager.LoadAccount();
        _loadManager.LoadPool();
        _loadManager.LoadScene();
    }

    private void OnAccountLoaded() {
        _loadManager.OnAccountLoaded -= OnAccountLoaded;
        Debug.Log("OnAccountLoaded!");
    }

    private void OnSceneReady() {
        _loadManager.OnSceneReady -= OnSceneReady;
        Debug.Log("OnSceneReady!");

        _loadManager.OnPlayFabAuthenticationSuccess -= OnPlayFabAuthenticationSuccess;
        _loadManager.OnPlayFabAuthenticationFailed -= OnPlayFabAuthenticationFailed;
        _loadManager.OnGPGSAuthenticationSuccess -= OnGPGSAuthenticationSuccess;
        _loadManager.OnGPGSAuthenticationFailed -= OnGPGSAuthenticationFailed;

        _loadManager.OpenLoadedScene();
    }

    private void OnPoolLoaded() {
        _loadManager.OnPoolLoaded -= OnPoolLoaded;
        Debug.Log("OnPoolLoaded!");
    }

    private void OnSceneActivated(Scene arg0, LoadSceneMode arg1) {
        SetGameState(GameState.MainMenu);

        UIManager.instance.OnUISceneChanged += OnUISceneChanged;
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
