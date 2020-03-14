using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectPooler))]
public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private Transform _leftPlatformPivot = null;
    [SerializeField]
    private Transform _rightPlatformPivot = null;
    [SerializeField]
    private Transform _player = null;
    [SerializeField]
    private GameObject _pauseMenuCanvas = null;
    [SerializeField]
    private Text _scoreText = null;

    private float _startPointOfPlayer;
    private int _score;

    public Transform LeftPlatformPivot { get { return _leftPlatformPivot; } }
    public Transform RightPlatformPivot { get { return _rightPlatformPivot; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("Platform");
        StartGame();
        _startPointOfPlayer = _player.transform.position.y;
    }

    private void StartGame() {
        Time.timeScale = 1f;
        _scoreText.text = "0";
        _score = 0;
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        _pauseMenuCanvas.SetActive(true);
    }

    public void ContinueGame() {
        Time.timeScale = 1f;
        _pauseMenuCanvas.SetActive(false);
    }

    public void GameOver() {
        throw new NotImplementedException();
    }

    public void SetScore() {
        if (_score < Math.Abs(_startPointOfPlayer - _player.transform.position.y)) {
            _score = (int)Math.Abs(_startPointOfPlayer - _player.transform.position.y);
            _scoreText.text = _score.ToString("0.##");
        }
    }
}
