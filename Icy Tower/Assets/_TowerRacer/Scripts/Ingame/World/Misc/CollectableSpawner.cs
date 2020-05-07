using System;
using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{

    #region Singleton

    public static CollectableSpawner instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private int _initialPowerUpSpawnFloor = 0;
    [SerializeField]
    private int _nextPowerUpSpawnFloor = 0;
    [SerializeField]
    private int _nextGoldSpawnFloor = 0;

    [SerializeField]
    private int _keySpawnFloor = 0;

    [Header("DEBUG")]
    [Utils.ReadOnly]
    [SerializeField]
    private bool _hasKeySpawned = false;

    public int NextPowerUpSpawnFloor
    {
        get { return _nextPowerUpSpawnFloor; }
    }
    public int NextGoldSpawnFloor
    {
        get { return _nextGoldSpawnFloor; }
    }

    public int KeySpawnFloor
    {
        get { return _keySpawnFloor; }
    }

    private void Start()
    {
        _nextGoldSpawnFloor = CalculateNextGoldOffset();
        _nextPowerUpSpawnFloor = _initialPowerUpSpawnFloor;
        _keySpawnFloor = CalculateKeySpawnFloor();
        PlatformManager.instance.OnWantedPlatformSpawnedForPowerUp += SpawnPowerUps;
        PlatformManager.instance.OnWantedPlatformSpawnedForGold += SpawnGolds;
        PlatformManager.instance.OnWantedPlatformSpawnedForKey += SpawnKey;
    }

    private void SpawnPowerUps(int floor)
    {
        Vector3 randomPosition = GetRandomSpawnPosition();

        ObjectPooler.instance.SpawnFromPool(GetRandomPowerUpToSpawn(), randomPosition);
        _nextPowerUpSpawnFloor += CalculateNextPowerUpOffset();
    }

    private void SpawnKey(int floor)
    {
        if (!_hasKeySpawned)
        {
            Vector3 randomPosition = GetRandomSpawnPosition();
            ObjectPooler.instance.SpawnFromPool("Key", randomPosition);
            _hasKeySpawned = true;
        }
    }

    private void SpawnGolds(int floor)
    {
        Vector3 randomPosition = GetRandomSpawnPosition();
        ObjectPooler.instance.SpawnFromPool(GetRandomGoldHolderType(), randomPosition);
        _nextGoldSpawnFloor += CalculateNextGoldOffset();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = WorldSettings.instance.GetRandomSpawnPosition().x;
        float y = PlatformManager.instance.GetLastSpawnedPlatformPosition().y + 1f;
        float z = -0.5f;
        return new Vector3(x, y, z);
    }

    private string GetRandomGoldHolderType()
    {
        int enumLenght = System.Enum.GetNames(typeof(GoldHolderTypes)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        return System.Enum.GetName(typeof(GoldHolderTypes), randomType);
    }

    private string GetRandomPowerUpToSpawn()
    {
        int enumLenght = System.Enum.GetNames(typeof(Collectables)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        return System.Enum.GetName(typeof(Collectables), randomType);
    }

    private int CalculateNextPowerUpOffset()
    {
        return UnityEngine.Random.Range(1, 5) + UnityEngine.Random.Range(0, 5);
    }

    private int CalculateNextGoldOffset()
    {
        return UnityEngine.Random.Range(1, 11);
    }

    private int CalculateKeySpawnFloor()
    {
        return UnityEngine.Random.Range(50, 60);
    }


    public void ResetGoldSpawnFloor()
    {
        _nextGoldSpawnFloor = CalculateNextGoldOffset();
        _nextPowerUpSpawnFloor = _initialPowerUpSpawnFloor;
        _keySpawnFloor = CalculateKeySpawnFloor();
        
     
    }

}
