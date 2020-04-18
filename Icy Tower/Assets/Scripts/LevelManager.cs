﻿using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour{

    [Header("Initializations")]
    [SerializeField]
    private int _countDownTime = 5;

    [SerializeField]
    [Utils.ReadOnly]
    private bool _isGamePaused = false;

    private bool _isUsingUmbrella = false;
    private bool _isUsingStickyPlumber = false;

    #region Singleton

    public static LevelManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public int CountDownTime {
        get { return _countDownTime; }
    }

    public bool IsUsingUmbrella {
        get { return _isUsingUmbrella; }
        set { _isUsingUmbrella = value; }
    }
    public bool IsUsingStickyPlumber {
        get { return _isUsingStickyPlumber; }
        set { _isUsingStickyPlumber= value; }
    }

    private IEnumerator IStartGameCountdown() {
        GameManager.instance.SetGameState(GameState.GameplayCountdown);

        yield return new WaitForSeconds(_countDownTime);

        StartGame();
    }

    private void StartGame() {
        SpawnManager.instance.SpawnAll();

        GameManager.instance.SetGameState(GameState.Gameplay);
        Account.instance.SetCurrentScore(0);
    }

    private void Pause() {
        Time.timeScale = 0;

        _isGamePaused = true;

        GameManager.instance.SetGameState(GameState.GamePaused);
    }

    private void Unpause() {
        Time.timeScale = 1;

        _isGamePaused = false;

        GameManager.instance.SetGameState(GameState.Gameplay);
    }

    public void OnClick_NewGame() {
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
    }

}
