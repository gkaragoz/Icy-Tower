using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformManager : MonoBehaviour {

    #region Singleton

    public static PlatformManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private float _initialSpawnPosition = 44f;
    [SerializeField]
    [Utils.ReadOnly]
    private PlatformStats _platformStats;
    [SerializeField]
    [Utils.ReadOnly]

    private int _floor = 0;
    private int _platformTypeIndex = 0;
    private Queue<Platform> _platforms = null;

    public Action<int> OnWantedPlatformSpawnedForPowerUp;
    public Action<int> OnWantedPlatformSpawnedForGold;
    public Action<int> OnWantedPlatformSpawnedForKey;

    public float InitialSpawnPosition {
        get { return _initialSpawnPosition; }
    }

    public int PlatformTypeIndex {
        get { return _platformTypeIndex; }
    }

    private void Start() {
        _platformStats = GetComponent<PlatformStats>();
    }

    public void SpawnPlatforms() {
        _floor = 0;
        _platformTypeIndex = 0;
        _platforms = new Queue<Platform>();

        for (int i = 0; i < ObjectPooler.instance.GetGameObjectsOnPool("Platform").Length; i++) {
            Platform platform = ObjectPooler.instance.SpawnFromPool("Platform").GetComponent<Platform>();
            platform.Floor = ++_floor;
            platform.SetText();
            platform.SetType(_platformTypeIndex);
            Vector3 scale = _platformStats.GetRandomScale();
            platform.SetScale(scale, _platformTypeIndex);
            platform.SetPosition(_platformStats.GetNewPosition(_initialSpawnPosition, _floor, scale.z),_platformTypeIndex);
            _platforms.Enqueue(platform);
            if (platform.Floor == CollectableSpawner.instance.NextPowerUpSpawnFloor)
                OnWantedPlatformSpawnedForPowerUp?.Invoke(platform.Floor);

            if (platform.Floor == CollectableSpawner.instance.NextGoldSpawnFloor)
                OnWantedPlatformSpawnedForGold?.Invoke(platform.Floor);

            if (platform.Floor == CollectableSpawner.instance.KeySpawnFloor)
                OnWantedPlatformSpawnedForKey?.Invoke(platform.Floor);
        }
    }

    public Platform SpawnPlatform() {
        Platform platform = _platforms.Dequeue();
        platform.Floor = ++_floor;
        if (platform.Floor % 100 == 0) {
            _platformTypeIndex++;
            Camera.main.GetComponent<NewCameraController>().IncreaseCameraSpeed(_platformTypeIndex * 0.16f);

        }
        platform.SetText();
        platform.SetType(_platformTypeIndex);
        Vector3 scale = _platformStats.GetRandomScale();
        platform.SetScale(scale, _platformTypeIndex);
        platform.SetPosition(_platformStats.GetNewPosition(_initialSpawnPosition, _floor, scale.z),_platformTypeIndex);

        _platforms.Enqueue(platform);
        if (platform.Floor == CollectableSpawner.instance.NextPowerUpSpawnFloor) 
            OnWantedPlatformSpawnedForPowerUp?.Invoke(platform.Floor);

        if (platform.Floor == CollectableSpawner.instance.NextGoldSpawnFloor) 
            OnWantedPlatformSpawnedForGold?.Invoke(platform.Floor);

        if (platform.Floor == CollectableSpawner.instance.KeySpawnFloor)
            OnWantedPlatformSpawnedForKey?.Invoke(platform.Floor);

        return platform;
    }

    public Vector3 GetLastSpawnedPlatformPosition() {
        return _platforms.ElementAt(_platforms.Count - 1).transform.position;
    }

    public Platform GetSpawnedPlatformAtFloor(int floor) {
        foreach (Platform platform in _platforms) {
            if (platform.Floor == floor)
                return platform;
        }
        return null;
    }

}
