using UnityEngine;

public class GoldHolder : MonoBehaviour , IPooledObject{

    [SerializeField]
    private Gold[] _golds;

    private void SetGolds() {
        foreach (var gold in _golds) {
            gold.CreateGold();
        }
    }

    public void OnObjectReused() {
        SetGolds();
        gameObject.SetActive(true);
    }
}
