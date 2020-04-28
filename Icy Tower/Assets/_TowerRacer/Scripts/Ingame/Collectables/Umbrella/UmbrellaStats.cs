using UnityEngine;

public class UmbrellaStats : MonoBehaviour{


    [Header("Initialization")]
    [SerializeField]
    private Umbrella_SO umbrella_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Umbrella_SO _umbrella = null;

    #region Initializations

    private void Awake() {
        if (umbrella_Template != null) {
            _umbrella = Instantiate(umbrella_Template);
        }
    }
    #endregion

    #region Setters

    public void SetName(string name) {
        _umbrella.Name = name;
    }

    public void SetDuration(float duration) {
        _umbrella.Duration = duration;
    }

    public void SetMoveSpeed(float speed) {
        _umbrella.MoveSpeed = speed;
    }

    #endregion

    #region Getters

    public string GetName() {
        return _umbrella.Name;
    }

    public GameObject GetPrefab() {
        return _umbrella.Prefab;
    }

    public float GetDuration() {
        return _umbrella.Duration;
    }

    public float GetMoveSpeed() {
        return _umbrella.MoveSpeed;
    }

    #endregion
}
