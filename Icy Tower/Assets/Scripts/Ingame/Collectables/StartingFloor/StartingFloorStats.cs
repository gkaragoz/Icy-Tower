using UnityEngine;

public class StartingFloorStats : MonoBehaviour{

    [Header("Initialization")]
    [SerializeField]
    private StartingFloorStats_SO _startingFloor_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private StartingFloorStats_SO _startingFloor = null;

    #region Initializations

    private void Awake() {
        if (_startingFloor_Template != null) {
            _startingFloor = Instantiate(_startingFloor_Template);
        }
    }
    #endregion

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
}
