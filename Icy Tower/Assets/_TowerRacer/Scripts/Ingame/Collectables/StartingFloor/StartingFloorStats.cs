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
        {
            Debug.Log("Burda:0"+_marketItem.GetCurrentLevel());
            return 0f;
        }
        Debug.Log("Start Pos :" + ((_marketItem.GetCurrentLevel() * 10f) * _platformStats.GetDistanceBetweenPlatforms()) + PlatformManager.instance.InitialSpawnPosition);
        return ((_marketItem.GetCurrentLevel() * 10f) * _platformStats.GetDistanceBetweenPlatforms() ) + PlatformManager.instance.InitialSpawnPosition;
    }

    #endregion
}
