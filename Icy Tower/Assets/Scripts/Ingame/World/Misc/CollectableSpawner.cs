using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour {
    [SerializeField]
    private int _initialPowerUpSpawnFloor = 50;
    private int _nextPowerUpSpawnFloor = 0;

    [SerializeField]
    private int _nextGoldSpawnFloor = 0;


    #region Singleton

    public static CollectableSpawner instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public int NextPowerUpSpawnFloor {
        get { return _nextPowerUpSpawnFloor; }
    }
    public int NextGoldSpawnFloor {
        get { return _nextGoldSpawnFloor; }
    }

    private void Start() {
        _nextGoldSpawnFloor = CalculateNextGoldOffset();
        _nextPowerUpSpawnFloor = _initialPowerUpSpawnFloor;
        PlatformManager.instance.OnWantedPlatformSpawnedForPowerUp += SpawnPowerUps;
        PlatformManager.instance.OnWantedPlatformSpawnedForGold += SpawnGolds;
    }

    private void SpawnPowerUps(int floor) {
        Vector3 randomPosition = GetRandomSpawnPosition();

        ObjectPooler.instance.SpawnFromPool(GetRandomPowerUpToSpawn(), randomPosition);
        _nextPowerUpSpawnFloor += CalculateNextPowerUpOffset();
    }

    private void SpawnGolds(int floor) {
        Vector3 randomPosition = GetRandomSpawnPosition();
        ObjectPooler.instance.SpawnFromPool(GetRandomGoldHolderType(), randomPosition);
        _nextGoldSpawnFloor += CalculateNextGoldOffset();
    }

    private Vector3 GetRandomSpawnPosition() {
        float x = WorldSettings.instance.GetRandomSpawnPosition().x;
        float y = PlatformManager.instance.GetLastSpawnedPlatformPosition().y + 1f;
        float z = -0.5f;
        return new Vector3(x, y, z);
    }

    private string GetRandomGoldHolderType() {
        int enumLenght = System.Enum.GetNames(typeof(GoldHolderTypes)).Length;
        int randomType = Random.Range(0, enumLenght);
        return System.Enum.GetName(typeof(GoldHolderTypes), randomType);
    }

    private string GetRandomPowerUpToSpawn() {
        int enumLenght = System.Enum.GetNames(typeof(Collectables)).Length;
        int randomType = Random.Range(0, enumLenght);
        return System.Enum.GetName(typeof(Collectables), randomType);
    }

    private int CalculateNextPowerUpOffset() {
        return (10 * Random.Range(1, 10)) + Random.Range(0,10);
    }

    private int CalculateNextGoldOffset() {
        return Random.Range(1, 11);
    }

}
