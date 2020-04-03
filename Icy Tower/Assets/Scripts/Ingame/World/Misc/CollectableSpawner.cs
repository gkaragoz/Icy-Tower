using System;
using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour {
    public bool IsRunning { get { return _isRunning; } }
    [SerializeField]
    private float _goldSpawnRate = 3f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isRunning = false;
    [SerializeField]
    [Utils.ReadOnly]
    private Coroutine _checkGoldCoroutine = null;

    private IEnumerator ICheckGolds() {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_goldSpawnRate);

        while (true) {
            yield return waitForSeconds;

            SpawnGold();
        }
    }

    private void SpawnGold() {
        Vector3 randomPosition = GetRandomSpawnPosition();

        ObjectPooler.instance.SpawnFromPool(GetRandomGoldType(), randomPosition, Quaternion.identity);
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
        string goldType = Enum.GetName(typeof(GoldHolderTypes), randomType);
        return goldType;
    }

    public void StartGoldSpawns() {
        if (_checkGoldCoroutine == null) {
            _checkGoldCoroutine = StartCoroutine(ICheckGolds());
            _isRunning = true;
        }
    }

    public void StopGoldSpawns() {
        StopCoroutine(_checkGoldCoroutine);
        _isRunning = false;
    }

}
