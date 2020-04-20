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

    private float _lastSpawnedWallPos = 30f;

    public void SpawnAll() {
        LastSpawnedWallPos = 30f;
        PlatformManager.instance.SpawnPlatforms();

        for (int i = 0; i < 5; i++) {
            SpawnWall();
        }
    }

    public void SpawnWall() {
        ObjectPooler.instance.SpawnFromPool("Wall", transform.position);
    }

    public float LastSpawnedWallPos {
        get { return _lastSpawnedWallPos; }
        set { _lastSpawnedWallPos = value; }
    }
}
