using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    private float _firstPlatformPosition = 0f;
    private void Start() {
        for (int i = 0; i < ObjectPooler.instance.GetGameObjectsOnPool("Platform").Length; i++) {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform() {
        Vector3 _randomPos = new Vector3(Random.Range(GameManager.instance.LeftMapSpawnTransform.position.x, GameManager.instance.RightMapSpawnTransform.position.x), //x
                                         _firstPlatformPosition += 4f,                                                                                                //y
                                         0f);                                                                                                                         //z
        
        ObjectPooler.instance.SpawnFromPool("Platform", _randomPos, Quaternion.identity);
    }
}
