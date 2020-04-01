using UnityEngine;

public class SpeedUpStats : MonoBehaviour{

    [Header("Initialization")]
    [SerializeField]
    private SpeedUp_SO speedUp_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private SpeedUp_SO _speedUp = null;

    #region Initializations

    private void Awake() {
        if (speedUp_Template != null) {
            _speedUp = Instantiate(speedUp_Template);
        }
    }
    #endregion

    #region Setters

    public void SetName(string name) {
        _speedUp.Name = name;
    }

    public void SetDuration(float duration) {
        _speedUp.Duration = duration;
    }

    public void SetSpeedAmount(float amount) {
        _speedUp.SpeedAmount = amount;
    }

    #endregion

    #region Getters

    public string GetName() {
        return _speedUp.Name;
    }

    public GameObject GetPrefab() {
        return _speedUp.Prefab;
    }

    public float GetDuration() {
        return _speedUp.Duration;
    }

    public float GetSpeedAmount() {
        return _speedUp.SpeedAmount;
    }

    #endregion
}
