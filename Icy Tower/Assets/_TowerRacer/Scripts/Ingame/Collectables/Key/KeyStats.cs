using UnityEngine;

public class KeyStats : MonoBehaviour {


    [Header("Initialization")]
    [SerializeField]
    private Key_SO _keyDefination_Template = null;

    [Header("Debug")]
    [Utils.ReadOnly]
    [SerializeField]
    private Key_SO _key = null;

    #region Initializations

    private void Awake() {
        if (_keyDefination_Template != null) {
            _key = Instantiate(_keyDefination_Template);
        }
    }

    #endregion

    #region Setters
    public void SetName(string name) {
        _key.Name = name;
    }

    public void SetAmount(int amount) {
        _key.Amount = amount;
    }
    #endregion

    #region Reporters

    public string GetName() {
        return _key.Name;
    }

    public GameObject GetPrefab() {
        return _key.Prefab;
    }

    public int GetAmount() {
        return _key.Amount;
    }
    #endregion

}
