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
        Vector3 randomPosition = new Vector3(
            Random.Range(GameManager.instance.LeftMapSpawnTransform.position.x, GameManager.instance.RightMapSpawnTransform.position.x),
            SpawnManager.instance.LastSpawnedPlatformPos + 1f,
            -0.1f);

        ObjectPooler.instance.SpawnFromPool("GanoverGold", randomPosition, Quaternion.identity);
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
