using System;
using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour{
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
        Vector3 randomPosition = RandomizePosition();

        ObjectPooler.instance.SpawnFromPool(GetRandomGoldType(), randomPosition, Quaternion.identity);
    }

    private Vector3 RandomizePosition() {
        return new Vector3(
                    UnityEngine.Random.Range(GameManager.instance.LeftMapSpawnTransform.position.x, GameManager.instance.RightMapSpawnTransform.position.x),
                    SpawnManager.instance.LastSpawnedPlatformPos + 2f,
                    -0.2f);
    }

    private string GetRandomGoldType() {
        int enumLenght = Enum.GetNames(typeof(GameManager.GoldTypes)).Length;
        int randomType = UnityEngine.Random.Range(0, enumLenght);
        string goldType = Enum.GetName(typeof(GameManager.GoldTypes), randomType);
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
