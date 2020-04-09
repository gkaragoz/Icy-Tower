using System;
using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour {
    public bool IsRunning { get { return _isRunning; } }
    [SerializeField]
    private float _goldSpawnRate = 3f;
    [SerializeField]
    private float _powerUpSpawnRate= 5f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isRunning = false;
    [SerializeField]
    [Utils.ReadOnly]
    private Coroutine _checkGoldCoroutine = null;
    [SerializeField]
    [Utils.ReadOnly]
    private Coroutine _checkPowerUpCoroutine = null;

    private IEnumerator ICheckGolds() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_goldSpawnRate);

        while (true) {
            yield return waitForSeconds;

            SpawnGold();
        }
    }

    private IEnumerator ICheckPowerUps() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_powerUpSpawnRate);

        while (true) {
            yield return waitForSeconds;

            SpawnPowerUps();
        }
    }

    private void SpawnGold() {
        Vector3 randomPosition = GetRandomSpawnPosition();

        ObjectPooler.instance.SpawnFromPool(GetRandomGoldType(), randomPosition, Quaternion.identity);
    }

    private void SpawnPowerUps() {
        Vector3 randomPosition = GetRandomSpawnPosition();

        ObjectPooler.instance.SpawnFromPool(GetRandomPowerUpToSpawn(), randomPosition);
    }

    private Vector3 GetRandomSpawnPosition() {
        float x = WorldSettings.instance.GetRandomPosition().x;
        float y = SpawnManager.instance.LastSpawnedPlatformPos + 1f;
        float z = -0.2f;
        return new Vector3(x, y, z);
    }

    private string GetRandomGoldType() {
        int enumLenght = Enum.GetNames(typeof(GoldHolderTypes)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        return Enum.GetName(typeof(GoldHolderTypes), randomType);
    }

    private string GetRandomPowerUpToSpawn() {
        int enumLenght = Enum.GetNames(typeof(Collectables)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        return Enum.GetName(typeof(Collectables), randomType);
    }

    public void StartGoldSpawns() {
        if (_checkGoldCoroutine == null) {
            _checkGoldCoroutine = StartCoroutine(ICheckGolds());
            _isRunning = true;
        }
    }

    public void StartPowerUpSpawns() {
        if (_checkPowerUpCoroutine == null) {
            _checkPowerUpCoroutine = StartCoroutine(ICheckPowerUps());
            _isRunning = true;
        }
    }

    public void StopGoldSpawns() {
        StopCoroutine(_checkGoldCoroutine);
        _isRunning = false;
    }

}
