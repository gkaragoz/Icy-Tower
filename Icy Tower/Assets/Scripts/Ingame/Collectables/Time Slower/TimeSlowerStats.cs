using UnityEngine;

public class TimeSlowerStats : MonoBehaviour{

    [Header("Initialization")]
    [SerializeField]
    private TimeSlower_SO timeSlower_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private TimeSlower_SO _timeSlower = null;

    #region Initializations

    private void Awake() {
        if (timeSlower_Template != null) {
            _timeSlower = Instantiate(timeSlower_Template);
        }
    }
    #endregion

    #region Setters

    public void SetName(string name) {
        _timeSlower.Name = name;
    }

    public void SetDuration(float duration) {
        _timeSlower.Duration = duration;
    }

    public void SetScrollSpeed(float amount) {
        _timeSlower.ScrollSpeed = amount;
    }

    public void SetSlowAmount(float amount) {
        _timeSlower.SlowAmount = amount;
    }

    #endregion

    #region Getters

    public string GetName() {
        return _timeSlower.Name;
    }

    public GameObject GetPrefab() {
        return _timeSlower.Prefab;
    }

    public float GetDuration() {
        return _timeSlower.Duration;
    }

    public float GetScrollSpeed() {
        return _timeSlower.ScrollSpeed;
    }

    public float GetSlowAmount() {
        return _timeSlower.SlowAmount;
    }

    #endregion
}
