using UnityEngine;

public class PlatformSaverStats : MonoBehaviour{

    [Header("Initialization")]
    [SerializeField]
    private PlatformSaver_SO _platformSaver_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private PlatformSaver_SO _platformSaver = null;

    #region Initializations

    private void Awake() {
        if (_platformSaver_Template != null) {
            _platformSaver = Instantiate(_platformSaver_Template);
        }
    }
    #endregion

    #region Setters

    public void SetName(string name) {
        _platformSaver.Name = name;
    }

    public void SetPlatformCount(int amount) {
        _platformSaver.PlatformCount = amount;
    }
    #endregion

    #region Getters

    public string GetName() {
        return _platformSaver.Name;
    }

    public GameObject GetPrefab() {
        return _platformSaver.Prefab;
    }

    public int GetPlatformCount() {
        return _platformSaver.PlatformCount;
    }
    #endregion
}
