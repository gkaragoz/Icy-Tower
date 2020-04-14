using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour{

    [Header("Initializations")]
    [SerializeField]
    private int _countDownTime = 3;

    public Action<GameState> OnGameStateChanged;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private GameState _gameState = GameState.MainMenu;

    [SerializeField]
    [Utils.ReadOnly]
    private bool _isGamePaused = false;

    #region Singleton

    public static LevelManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

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

    private IEnumerator IStartGameCountdown() {
        GameStateEnum = GameState.GameplayCountdown;
        yield return new WaitForSeconds(_countDownTime);

        StartGame();
    }

    private void StartGame() {
        SpawnManager.instance.SpawnAll();
        GameStateEnum = GameState.Gameplay;

      //  Camera.main.GetComponent<CameraController>().scrollSpeed = 0f;
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
        SceneManager.LoadScene("Scene");
        OnClick_NewGame();

        GameStateEnum = GameState.RestartGame;
    }

}
