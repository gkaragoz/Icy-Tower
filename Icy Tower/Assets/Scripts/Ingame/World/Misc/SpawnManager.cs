using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    #region Singleton

    public static SpawnManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    private float _lastSpawnedPlatformPos = 44f;

    private float _lastSpawnedWallPos = 30f;

    public void SpawnAll() {
        for (int i = 0; i < 10; i++) {
            SpawnPlatform();
        }

        for (int i = 0; i < 5; i++) {
            SpawnWall();
        }
    }

    public void SpawnPlatform() {
        ObjectPooler.instance.SpawnFromPool("Platform");
    }

    public void SpawnWall() {
        ObjectPooler.instance.SpawnFromPool("Wall", transform.position);
    }

    public float LastSpawnedPlatformPos {
        get { return _lastSpawnedPlatformPos; }
        set { _lastSpawnedPlatformPos = value; }
    }

    public float LastSpawnedWallPos {
        get { return _lastSpawnedWallPos; }
        set { _lastSpawnedWallPos = value; }
    }
}
