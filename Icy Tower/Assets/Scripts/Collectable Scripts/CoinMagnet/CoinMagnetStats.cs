using UnityEngine;

public class CoinMagnetStats : MonoBehaviour{

    [Header("Initialization")]
    [SerializeField]
    private CoinMagnet_SO _coinMagnet_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CoinMagnet_SO _coinMagnet = null;


    #region Initializations

    private void Awake() {
        if (_coinMagnet_Template != null) {
            _coinMagnet = Instantiate(_coinMagnet_Template);
        }
    }
    #endregion

    #region Setters

    public void SetName(string name) {
        _coinMagnet.Name = name;
    }

    public void SetDuration(float duration) {
        _coinMagnet.Duration = duration;
    }

    public void SetRadius(float radius) {
        _coinMagnet.Radius = radius;
    }

    #endregion

    #region Getters

    public string GetName() {
        return _coinMagnet.Name;
    }

    public GameObject GetPrefab() {
        return _coinMagnet.Prefab;
    }

    public float GetDuration() {
        return _coinMagnet.Duration;
    }

    public float GetRadius() {
        return _coinMagnet.Radius;
    }

    #endregion


}
