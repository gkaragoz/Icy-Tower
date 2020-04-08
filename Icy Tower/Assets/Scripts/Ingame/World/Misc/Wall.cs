using System;
using UnityEngine;

public class Wall : MonoBehaviour, IPooledObject {

    [Obsolete]
    public void OnObjectReused() {
        gameObject.SetActiveRecursively(true);
        SetWallPosition();
    }

    private void SetWallPosition() {
        transform.position = new Vector3(0, SpawnManager.instance.LastSpawnedWallPos += 35, 0);
    }
}
