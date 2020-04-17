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

    public float GetMaxScale() {
        return _platform.MaxScale;
    }
    public float GetMinScale() {
        return _platform.MinScale;
    }

    public float GetThickness() {
        return _platform.Thickness;
    }

    public float GetDepth() {
        return _platform.Depth;
    }

    public float GetDistanceBetweenPlatforms() {
        return _platform.DistanceBetweenPlatforms;
    }


    #endregion

    #region Custom Methods
    public Vector3 GetRandomScale() {
        return new Vector3(GetDepth(), GetThickness(), Random.Range(GetMinScale(), GetMaxScale())) ;
    }

    public Vector3 GetNewPosition(float initialSpawnPos, int lastSpawnedPlatform,float scaleZ){
        float x =WorldSettings.instance.GetRandomBorderPosition().x;
        float y = initialSpawnPos + (GetDistanceBetweenPlatforms() * lastSpawnedPlatform);
        float z = 0;

        if(x >= 0) {
            float endPosX = x + (scaleZ / 2);
            if((endPosX) > WorldSettings.instance.GetMapRightBorderPosition().x) {
                float distance = endPosX - WorldSettings.instance.GetMapRightBorderPosition().x;
                x -= distance;
            }
        } else {
            float endPosX = x - (scaleZ / 2); 
            if ((endPosX) < WorldSettings.instance.GetMapLeftBorderPosition().x) {
                float distance = endPosX - WorldSettings.instance.GetMapLeftBorderPosition().x;
                x -= distance;
            }
        }
        return new Vector3(x, y, z);
    }
    #endregion
}
