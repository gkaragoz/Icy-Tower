using System;
using System.Collections.Generic;
using UnityEngine;

public class GoldHolder : MonoBehaviour, IPooledObject {

    [SerializeField]
    private Gold[] _golds = null;
    private Vector3[] _positions;
    private bool isFirstSpawn = true;

    private void SetGolds() {
        foreach (var gold in _golds) {
            gold.CreateGold();
        }
    }

    private void SaveGoldPositions() {
        _positions = new Vector3[_golds.Length];
        for (int i = 0; i < _golds.Length; i++) {
            _positions[i] = _golds[i].transform.localPosition;
        }
    }

    private void SetGoldPositions() {
        for (int i = 0; i < _golds.Length; i++) {
            _golds[i].transform.localPosition = _positions[i];
        }
    }

    [System.Obsolete]
    public void OnObjectReused() {
        if (isFirstSpawn) {
            SaveGoldPositions();
            isFirstSpawn = false;
        } else {
            SetGoldPositions();
        }
        SetGolds();
        gameObject.SetActiveRecursively(true);
    }

}
