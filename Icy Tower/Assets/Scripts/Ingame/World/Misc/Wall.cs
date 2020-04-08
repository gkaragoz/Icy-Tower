using System;
using UnityEngine;

public class Wall : MonoBehaviour, IPooledObject {

    [SerializeField]
    private Transform[] _holderPos = null;

    [Obsolete]
    public void OnObjectReused() {
        gameObject.SetActiveRecursively(true);
        SetWallPosition();
        FillTheWallWithTowerEnvironments();
    }

    private void SetWallPosition() {
        transform.position = new Vector3(0, SpawnManager.instance.LastSpawnedWallPos += 35, 0);
    }

    private void FillTheWallWithTowerEnvironments() {
        int _randPos;
        for (int i = 0; i < 3; i++) {
            _randPos = UnityEngine.Random.Range(0, _holderPos.Length);
            SpawnManager.instance.SpawnTowerEnvironment(_holderPos[_randPos].transform.position, _holderPos[_randPos].transform.rotation);
        }
    }
}