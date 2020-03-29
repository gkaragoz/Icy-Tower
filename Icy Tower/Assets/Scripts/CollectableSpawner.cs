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
        return new Vector3(
                    UnityEngine.Random.Range(GameManager.instance.LeftMapSpawnTransform.position.x, GameManager.instance.RightMapSpawnTransform.position.x),
                    SpawnManager.instance.LastSpawnedPlatformPos + 1f,
                    -0.2f);
    }

    private string GetRandomGoldType() {
        int enumLenght = Enum.GetNames(typeof(GameManager.GoldHolderTypes)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        string goldType = Enum.GetName(typeof(GameManager.GoldHolderTypes), randomType);
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
