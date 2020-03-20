using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private float _gravityScale = 1.0f;
    [SerializeField]
    private Transform _leftMapSpawnTransform = null;
    [SerializeField]
    private Transform _rightMapSpawnTransform = null;

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
    }
}
