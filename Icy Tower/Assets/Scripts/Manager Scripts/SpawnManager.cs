using System;
using System.Collections;
using System.Collections.Generic;
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

    private float _lastSpawnedPlatformPos = 0f;

    private void Start() {
        for (int i = 0; i < ObjectPooler.instance.GetGameObjectsOnPool("Platform").Length; i++) {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform() {
        ObjectPooler.instance.SpawnFromPool("Platform" , Quaternion.identity);
    }

    public float LastSpawnedPlatformPos {
        get { return _lastSpawnedPlatformPos; }
        set { _lastSpawnedPlatformPos = value; }
    }
}
