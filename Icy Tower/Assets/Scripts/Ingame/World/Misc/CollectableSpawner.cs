using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour {

    #region Singleton

    public static CollectableSpawner instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion


    private void SpawnPowerUps() {
        Vector3 randomPosition = GetRandomSpawnPosition();

        ObjectPooler.instance.SpawnFromPool(GetRandomPowerUpToSpawn(), randomPosition);
    }

    private Vector3 GetRandomSpawnPosition() {
        float x = WorldSettings.instance.GetRandomSpawnPosition().x;
        float y = PlatformManager.instance.GetLastSpawnedPlatformPosition().y + 1f;
        float z = -0.5f;
        return new Vector3(x, y, z);
    }

    private string GetRandomGoldHolderType() {
        int enumLenght = System.Enum.GetNames(typeof(GoldHolderTypes)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        return System.Enum.GetName(typeof(GoldHolderTypes), randomType);
    }

    private string GetRandomPowerUpToSpawn() {
        int enumLenght = System.Enum.GetNames(typeof(Collectables)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        return System.Enum.GetName(typeof(Collectables), randomType);
    }

}
