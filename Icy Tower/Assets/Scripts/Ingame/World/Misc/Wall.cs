using UnityEngine;

public class Wall : MonoBehaviour, IPooledObject {

    [SerializeField]
    private GameObject _skullsParent = null;
    [SerializeField]
    private GameObject _torchesParent = null;
    [SerializeField]
    private GameObject[] _windowsParent = null;
    [SerializeField]
    private GameObject[] _wellsParent = null;
    [SerializeField]
    private GameObject[] _seperatorsParent = null;
    [SerializeField]
    private GameObject[] _wallTypesParent = null;

    public void OnObjectReused() {
        gameObject.SetActive(true);
        SetWallPosition();
        ActivateProps();
        ActivateWindows();
        AcivateWells();
        AcivateSeperators();
        AcivateWallType();
    }

    private void SetWallPosition() {
        transform.position = new Vector3(0, SpawnManager.instance.LastSpawnedWallPos += 35, 0);
    }

    private void ActivateWindows() {
        for (int ii = 0; ii < _windowsParent.Length; ii++) {
            _windowsParent[ii].SetActive(false);
        }
        int random = Random.Range(0, _windowsParent.Length);

        _windowsParent[random].SetActive(true);
    }

    private void AcivateWells() {
        for (int ii = 0; ii < _wellsParent.Length; ii++) {
            _wellsParent[ii].SetActive(false);
        }
        int random = Random.Range(0, _wellsParent.Length);

        _wellsParent[random].SetActive(true);
    }

    private void AcivateSeperators() {
        for (int ii = 0; ii < _seperatorsParent.Length; ii++) {
            _seperatorsParent[ii].SetActive(false);
        }
        int random = Random.Range(0, _seperatorsParent.Length);

        _seperatorsParent[random].SetActive(true);
    }

    private void AcivateWallType() {
        for (int ii = 0; ii < _wallTypesParent.Length; ii++) {
            _wallTypesParent[ii].SetActive(false);
        }
        int random = Random.Range(0, _wallTypesParent.Length);

        _wallTypesParent[random].SetActive(true);
    }

    private void ActivateProps() {
        int dice = Random.Range(0, 2);
        if (dice == 0) {
            _skullsParent.SetActive(true);
            _torchesParent.SetActive(false);
        } else {
            _skullsParent.SetActive(false);
            _torchesParent.SetActive(true);
        }
    }

}
