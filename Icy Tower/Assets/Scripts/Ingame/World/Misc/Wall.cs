using System;
using UnityEngine;

public class Wall : MonoBehaviour, IPooledObject {

    [SerializeField]
    private Transform[] _windowsHolderPos = null;
    [SerializeField]
    private Transform[] _propHolder = null;
    [SerializeField]
    private int _windowSpawnRate = 0;
    [SerializeField]
    private int _propSpawnRate = 0;

    [Obsolete]
    public void OnObjectReused() {
        gameObject.SetActiveRecursively(true);
        SpawnTowerProps();
        SetWallPosition();
    }

    private void SetWallPosition() {
        transform.position = new Vector3(0, SpawnManager.instance.LastSpawnedWallPos += 35, 0);
    }

    private void SpawnTowerProps() {
        int randomNumber;
        for (int i = 0; i < _windowSpawnRate; i++) {
            randomNumber = GetRandomNumber(0, _windowsHolderPos.Length);
            GameObject window = SpawnManager.instance.SpawnTowerWindow();
            window.transform.parent = GetRandomWindowHolderTransform(randomNumber);
            window.transform.localPosition = Vector3.zero;
            window.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        for (int i = 0; i < _propSpawnRate; i++) {
            randomNumber = GetRandomNumber(0, _propHolder.Length);
            GameObject prop = SpawnManager.instance.SpawnTowerProp();
            prop.transform.parent = GetRandomPropTransform(randomNumber);
            prop.transform.localPosition = Vector3.zero;
            prop.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private Transform GetRandomWindowHolderTransform(int rand) {
        return _windowsHolderPos[rand].transform;
    }

    private Transform GetRandomPropTransform(int rand) {
        return _propHolder[rand].transform;
    }

    private int GetRandomNumber(int min, int max) {
        return UnityEngine.Random.Range(min, max);
    }
}
