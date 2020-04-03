using UnityEngine;

public class SuperCoinStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private SuperCoin_SO _superCoin_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private SuperCoin_SO _superCoin = null;

    #region Initializations

    private void Awake() {
        if (_superCoin_Template != null) {
            _superCoin = Instantiate(_superCoin_Template);
        }
    }
    #endregion

    #region Setters

    public void SetName(string name) {
        _superCoin.Name = name;
    }

    public void SetMoveSpeed(int amount) {
        _superCoin.Amount = amount;
    }

    #endregion

    #region Getters

    public string GetName() {
        return _superCoin.Name;
    }

    public GameObject GetPrefab() {
        return _superCoin.Prefab;
    }

    public int GetAmount() {
        return _superCoin.Amount;
    }

    #endregion

}
