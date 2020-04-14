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

    public float InitialSpawnPosition {
        get { return _initialSpawnPosition; }
    }


    private void Start() {
        _platformStats = GetComponent<PlatformStats>();
    }

    public void SpawnPlatforms() {
        _platforms = new Queue<Platform>();
        for (int i = 0; i < ObjectPooler.instance.GetGameObjectsOnPool("Platform").Length; i++) {
            Platform platform = ObjectPooler.instance.SpawnFromPool("Platform").GetComponent<Platform>();
            platform.Floor = ++_floor;
            platform.SetText();
            platform.SetType(_platformTypeIndex);
            platform.SetScale(_platformStats.GetRandomScale());
            platform.SetPosition(_platformStats.GetNewPosition(_initialSpawnPosition, _floor, platform.gameObject.transform.localScale.x));
            _platforms.Enqueue(platform);
        }
    }

    public Platform SpawnPlatform() {
        Platform platform = _platforms.Dequeue();
        platform.Floor = ++_floor;
        if (platform.Floor % 100 == 0) {
            _platformTypeIndex++;
        }
        platform.SetText();
        platform.SetType(_platformTypeIndex);
        platform.SetScale(_platformStats.GetRandomScale());
        platform.SetPosition(_platformStats.GetNewPosition(_initialSpawnPosition, _floor, platform.gameObject.transform.localScale.x));
        _platforms.Enqueue(platform);
        return platform;
    }

    public Vector3 GetLastSpawnedPlatformPosition() {
        return _platforms.ElementAt(_platforms.Count - 1).transform.position;
    }

    public Vector3 GetSpawnedPlatformPositionAtFloor(int floor) {
        
        foreach (Platform platform in _platforms) {
            if (platform.Floor == floor)
                return platform.transform.position;
        }
        return Vector3.zero;
    }
}
