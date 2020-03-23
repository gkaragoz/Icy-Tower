using System;
using UnityEngine;

public class Wall : MonoBehaviour, IPooledObject {

    public void OnObjectReused() {
        gameObject.SetActive(true);
        SetWallPosition();
    }

   

    private void SetWallPosition() {
        transform.position = new Vector3(0, SpawnManager.instance.LastSpawnedWallPos += 10, 0 );
    }
}
