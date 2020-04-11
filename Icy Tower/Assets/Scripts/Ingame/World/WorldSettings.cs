using UnityEngine;

public class WorldSettings : MonoBehaviour {

    #region Singleton

    public static WorldSettings instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Init();
    }

    #endregion

    [Header("Initialization")]
    [SerializeField]
    private Transform _leftMapSpawnTransform = null;
    [SerializeField]
    private Transform _rightMapSpawnTransform = null;
    [SerializeField]
    private Transform _leftMapBorderTransform = null;
    [SerializeField]
    private Transform _rightMapBorderTransform = null;
    [SerializeField]
    private WorldSettings_SO _worldSettings_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private WorldSettings_SO _worldSettings = null;

    #region Initializations

    private void Init() {
        if (_worldSettings_Template != null) {
            _worldSettings = Instantiate(_worldSettings_Template);
        }

        SetMapLeftSpawnPosition();
        SetMapRightSpawnPosition();
        SetMapLeftBorderPosition();
        SetMapRightBorderPosition();
    }
    #endregion

    #region Setters

    public void SetMapLeftSpawnPosition() {
        _worldSettings.MapLeftSpawnTransform = _leftMapSpawnTransform;
    }

    public void SetMapRightSpawnPosition() {
        _worldSettings.MapRightSpawnTransform = _rightMapSpawnTransform;
    }

    public void SetMapLeftBorderPosition() {
        _worldSettings.MapLeftBorderTransform = _leftMapBorderTransform;
    }

    public void SetMapRightBorderPosition() {
        _worldSettings.MapRightBorderTransform = _rightMapBorderTransform;
    }

    #endregion

    #region Getters

    public Vector3 GetMapLeftSpawnPosition() {
        return _worldSettings.MapLeftSpawnTransform.position;
    }


    public Vector3 GetMapRightSpawnPosition() {
        return _worldSettings.MapRightSpawnTransform.position;
    }
    public Vector3 GetMapLeftBorderPosition() {
        return _worldSettings.MapLeftBorderTransform.position;
    }

    public Vector3 GetMapRightBorderPosition() {
        return _worldSettings.MapRightBorderTransform.position;
    }

    public Vector3 GetRandomSpawnPosition() {
        float x = Random.Range(GetMapLeftSpawnPosition().x, GetMapRightSpawnPosition().x);
        float y = Random.Range(GetMapLeftSpawnPosition().y, GetMapRightSpawnPosition().y);
        float z = Random.Range(GetMapLeftSpawnPosition().z, GetMapRightSpawnPosition().z);
        return new Vector3(x, y, z);
    }

    public Vector3 GetRandomBorderPosition() {
        float x = Random.Range(GetMapLeftBorderPosition().x, GetMapRightBorderPosition().x);
        float y = Random.Range(GetMapLeftBorderPosition().y, GetMapRightBorderPosition().y);
        float z = Random.Range(GetMapLeftBorderPosition().z, GetMapRightBorderPosition().z);
        return new Vector3(x, y, z);
    }

    #endregion

}
