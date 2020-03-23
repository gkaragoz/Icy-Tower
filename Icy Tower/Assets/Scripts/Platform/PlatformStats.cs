using UnityEngine;

public class PlatformStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private PlatformStats_SO _platformDefinition_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private PlatformStats_SO _platform = null;

    #region Initializations

    private void Awake() {
        if (_platformDefinition_Template != null) {
            _platform = Instantiate(_platformDefinition_Template);
        }
    }
    #endregion


    #region Setters

    public void SetMovementSpeed(float speed) {
        _platform.MovementSpeed = speed;
    }

    public void SetJumpPower(int power) {
        _platform.JumpPower = power;
    }

    public void SetMaxScale(int amount) {
        _platform.MaxScale = amount;
    }
    public void SetMinScale(int amount) {
        _platform.MinScale = amount;
    }

    public void SetThickness(float amount) {
        _platform.Thickness = amount;
    }

    public void SetDistanceBetweenPlatform(float amount) {
        _platform.DistanceBetweenPlatforms = amount;
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _platform.name;
    }

    public GameObject GetPrefab() {
        return _platform.Prefab;
    }

    public float GetMovementSpeed() {
        return _platform.MovementSpeed;
    }

    public int GetJumpPower() {
        return _platform.JumpPower;
    }

    public int GetMaxScale() {
        return _platform.MaxScale;
    }
    public int GetMinScale() {
        return _platform.MinScale;
    }

    public float GetThickness() {
        return _platform.Thickness;
    }

    public float GetDistanceBetweenPlatforms() {
        return _platform.DistanceBetweenPlatforms;
    }


    #endregion

    #region Custom Methods


    #endregion
}
