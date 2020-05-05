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
    private int _currentLevel;
    #region Custom Methods

    private void Start()
    {
        _marketItem = MarketManager.instance.GetMarketItem(_marketItem.GetId());
        _marketItem.OnMarketItemUpdated += CalculateNewStats;

    }
   void CalculateNewStats()
    {
        _currentLevel = _marketItem.GetCurrentLevel();
    }

    public float CalculateStartingPlatformPosition() {
        if (_currentLevel == 0)
        {
            Debug.Log("Burda:0"+ _currentLevel);
            return 0f;
        }
        Debug.Log("Start Pos :" + ((_currentLevel * 10f) * _platformStats.GetDistanceBetweenPlatforms()) + PlatformManager.instance.InitialSpawnPosition);
        return ((_currentLevel * 10f) * _platformStats.GetDistanceBetweenPlatforms() ) + PlatformManager.instance.InitialSpawnPosition;
    }

    #endregion
}
