using System;
using UnityEngine;

public class Wall : MonoBehaviour, IPooledObject {

    [SerializeField]
    private GameObject _skullsParent = null;
    [SerializeField]
    private GameObject _torchesParent = null;
    [SerializeField]
    private GameObject[] _windowsParent = null;
    [Obsolete]
    public void OnObjectReused() {
        gameObject.SetActiveRecursively(true);
        SetWallPosition();
        ActivateProps();
        ActivateWindows();
    }

    private void SetWallPosition() {
        transform.position = new Vector3(0, SpawnManager.instance.LastSpawnedWallPos += 35, 0);
        Debug.Log("Position : " + transform.position);
    }

    private void ActivateWindows() {
        for (int ii = 0; ii < _windowsParent.Length; ii++) {
            _windowsParent[ii].SetActive(false);
        }
        int random = UnityEngine.Random.Range(0, _windowsParent.Length);

        _windowsParent[random].SetActive(true);
    }

    private void ActivateProps() {
        int dice = UnityEngine.Random.Range(0, 2);
        if (dice == 0) {
            _skullsParent.SetActive(true);
            _torchesParent.SetActive(false);
        } else {
            _skullsParent.SetActive(false);
            _torchesParent.SetActive(true);
        }
    }

}
