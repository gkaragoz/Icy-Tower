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
    private Transform _leftMapTransform = null;
    [SerializeField]
    private Transform _rightMapTransform = null;
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

        SetMapLeftBorder();
        SetMapRightBorder();
    }
    #endregion

    #region Setters

    public void SetMapLeftBorder() {
        _worldSettings.MapLeftBorder = _leftMapTransform;
    }

    public void SetMapRightBorder() {
        _worldSettings.MapRightBorder = _rightMapTransform;
    }

    #endregion

    #region Getters

    public Vector3 GetMapLeftBorder() {
        return _worldSettings.MapLeftBorder.position;
    }

    public Vector3 GetMapRightBorder() {
        return _worldSettings.MapRightBorder.position;
    }

    public Vector3 GetRandomPosition() {
        float x = Random.Range(GetMapLeftBorder().x, GetMapRightBorder().x);
        float y = Random.Range(GetMapLeftBorder().y, GetMapRightBorder().y);
        float z = Random.Range(GetMapLeftBorder().z, GetMapRightBorder().z);
        return new Vector3(x, y, z);
    }

    #endregion

}
