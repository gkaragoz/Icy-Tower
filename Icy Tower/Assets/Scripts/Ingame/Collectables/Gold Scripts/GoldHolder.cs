using UnityEngine;

public class GoldHolder : MonoBehaviour, IPooledObject {

    private Gold[] _golds = null;

    private void Awake() {
        _golds = GetComponentsInChildren<Gold>();
        ActivateGolds(false);
    }

    private void ActivateGolds(bool isActive) {
        for (int i = 0; i < _golds.Length; i++) {
            _golds[i].SetVisibility(isActive);
        }
    }

    public void OnObjectReused() {
        ActivateGolds(true);
    }

}
