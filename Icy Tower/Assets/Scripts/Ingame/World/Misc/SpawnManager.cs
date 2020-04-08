﻿using System;
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

    private float _lastSpawnedWallPos = -35;


    public void SpawnAll() {
        for (int i = 0; i < ObjectPooler.instance.GetGameObjectsOnPool("Platform").Length; i++) {
            SpawnPlatform();
        }

        for (int i = 0; i < ObjectPooler.instance.GetGameObjectsOnPool("Wall").Length; i++) {
            SpawnWall();
        }
    }

    public void SpawnPlatform() {
        ObjectPooler.instance.SpawnFromPool("Platform" , Quaternion.identity);
    }

    public void SpawnWall() {
        ObjectPooler.instance.SpawnFromPool("Wall", transform.position,Quaternion.identity);
    }

    public void SpawnTowerEnvironment(Vector3 pos,Quaternion rotation) {
       ObjectPooler.instance.SpawnFromPool(GetRandomTowerEnvironments(), pos, rotation);
    }

    public string GetRandomTowerEnvironments() {
        int enumLenght = Enum.GetNames(typeof(TowerEnvironments)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        return Enum.GetName(typeof(TowerEnvironments), randomType);
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
