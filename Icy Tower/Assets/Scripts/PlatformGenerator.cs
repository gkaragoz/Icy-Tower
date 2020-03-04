using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    #region Singleton

    public static PlatformGenerator instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private float _lastPlatformY = -4f;

    private void Start() {
        for (int i = 0; i < ObjectPooler.instance.GetGameObjectsOnPool("Platform").Length; i++) {
            SpawnPlatform();
        }
    }

    //private IEnumerator ICheckPlatforms() {
    //    WaitForSeconds waitForSeconds = new WaitForSeconds(.5f);

    //    while (true) {
    //        yield return waitForSeconds;

    //        SpawnPlatform();
    //    }
    //}

    public void SpawnPlatform() {
        Vector3 randomPosition = new Vector3(
            Random.Range(GameManager.instance.LeftPlatformPivot.position.x, GameManager.instance.RightPlatformPivot.position.x),
              _lastPlatformY += 2f,
              0);
        ObjectPooler.instance.SpawnFromPool("Platform", randomPosition, Quaternion.identity);


        Debug.Log("Platform has been spawned");
    }
}
