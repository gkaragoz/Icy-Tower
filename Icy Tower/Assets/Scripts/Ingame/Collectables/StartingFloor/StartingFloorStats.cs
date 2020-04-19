using UnityEngine;

public class StartingFloorStats : MonoBehaviour{

    #region Singleton

    public static StartingFloorStats instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private StartingFloorStats_SO _startingFloor = null;

    [SerializeField]
    private PlatformStats _platformStats = null;

    #region Setters

    public void SetName(string name) {
        _startingFloor.Name = name;
    }
    public void SetLevel(int level) {
        _startingFloor.Level = level;
    }
    
    public void SetFloorMultiplier(int multiplier) {
        _startingFloor.FloorMultiplier = multiplier;
    }

    #endregion

    #region Increasers

    public void LevelUp() {
        _startingFloor.Level += 1;
    }

    #endregion

    #region Getters

    public string GetName() {
        return _startingFloor.Name;
    }

    public int GetLevel() {
        return _startingFloor.Level;
    }

    public int GetFloorMultiplier() {
        return _startingFloor.FloorMultiplier;
    }
    #endregion

    #region

    public float CalculateStartingPlatformPosition() {
        if (_startingFloor.Level == 0)
            return 0f; 

        return ((_startingFloor.Level * _startingFloor.FloorMultiplier) * _platformStats.GetDistanceBetweenPlatforms() ) + PlatformManager.instance.InitialSpawnPosition;
    }

    #endregion
}
