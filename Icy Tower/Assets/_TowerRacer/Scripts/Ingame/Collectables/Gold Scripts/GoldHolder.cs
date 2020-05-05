using UnityEngine;

public class GoldHolder : MonoBehaviour, IPooledObject {

    private Gold[] _golds = null;
    [SerializeField]
    MarketItem _marketItem;
    private void Awake() {
        _marketItem = MarketManager.instance.GetMarketItem(_marketItem.GetId());
        _marketItem.OnMarketItemUpdated += CalculateNewStats;
        _golds = GetComponentsInChildren<Gold>();
        ActivateGolds(false);
        CalculateNewStats();


    }

    private void ActivateGolds(bool isActive) {
        for (int i = 0; i < _golds.Length; i++) {
            _golds[i].SetVisibility(isActive);
            _golds[i].SetColorfulGoldValue(_marketItem.GetCurrentLevel());
        }
    }

    public void OnObjectReused() {
        ActivateGolds(true);
    }


    private void CalculateNewStats()
    {
        for (int i = 0; i < _golds.Length; i++)
        {
            _golds[i].SetColorfulGoldValue(_marketItem.GetCurrentLevel());
        }
    }

}
