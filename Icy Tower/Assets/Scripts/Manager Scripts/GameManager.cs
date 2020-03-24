using System;
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
    private Transform _camera = null;
    [SerializeField]
    private PlayerController _playerController = null;

    public Action<PlayerStats> OnPlayerStatsChanged;

    [Header("Debug")]
    [Utils.ReadOnly]
    [SerializeField]
    private float _gravityScale = 1.0f;
    #region Singleton

    public static GameManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public float GetGravityScale() {
        return _gravityScale;
    }

    public void SetGravityScale(float value) {
        _gravityScale = value;
    }

    public Transform LeftMapSpawnTransform { get { return _leftMapSpawnTransform; } }
    public Transform RightMapSpawnTransform { get { return _rightMapSpawnTransform; } }

    private void Start() {
        ObjectPooler.instance.InitializePool("Platform");
        ObjectPooler.instance.InitializePool("Wall");
        ObjectPooler.instance.InitializePool("GanoverGold");
        _collectableSpawner.StartGoldSpawns();
    }

    public void AddGoldToPlayer() {
        _playerController.AddGold();

        OnPlayerStatsChanged?.Invoke(_playerController.PlayerStats);
    }
}
