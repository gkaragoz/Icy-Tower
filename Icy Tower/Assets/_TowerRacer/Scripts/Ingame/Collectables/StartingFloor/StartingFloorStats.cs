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
    private MarketItem _marketItem= null;

    [SerializeField]
    private PlatformStats _platformStats = null;

    #region Custom Methods

    public float CalculateStartingPlatformPosition() {
        if (_marketItem.GetCurrentLevel() == 0)
            return 0f; 

        return ((_marketItem.GetCurrentLevel() * 10f) * _platformStats.GetDistanceBetweenPlatforms() ) + PlatformManager.instance.InitialSpawnPosition;
    }

    #endregion
}
