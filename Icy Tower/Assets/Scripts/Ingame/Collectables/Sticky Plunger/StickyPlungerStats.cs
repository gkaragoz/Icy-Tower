using UnityEngine;

public class StickyPlungerStats : MonoBehaviour{

    [Header("Initialization")]
    [SerializeField]
    private StickyPlunger_SO stickyPlunger_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private StickyPlunger_SO _stickyPlunger = null;

    #region Initializations

    private void Awake() {
        if (stickyPlunger_Template != null) {
            _stickyPlunger = Instantiate(stickyPlunger_Template);
        }
    }
    #endregion

    #region Setters

    public void SetName(string name) {
        _stickyPlunger.Name = name;
    }

    public void SetDuration(float duration) {
        _stickyPlunger.Duration = duration;
    }

    public void SetMoveSpeed(float speed) {
        _stickyPlunger.MoveSpeed = speed;
    }

    #endregion

    #region Getters

    public string GetName() {
        return _stickyPlunger.Name;
    }

    public GameObject GetPrefab() {
        return _stickyPlunger.Prefab;
    }

    public float GetDuration() {
        return _stickyPlunger.Duration;
    }

    public float GetMoveSpeed() {
        return _stickyPlunger.MoveSpeed;
    }

    #endregion
}
