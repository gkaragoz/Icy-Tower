using System;
using UnityEngine;

public class Market : MonoBehaviour {

    #region Singleton

    public static Market instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public Action<int, int> OnBuyItem;
    public Action OnMarketInitialized;

    [Header("Debug")]
    [Utils.ReadOnly]
    [SerializeField]
    private Market_SO _market = null;

    public void Init(Market_SO marketSO) {
        this._market = marketSO;

        OnMarketInitialized.Invoke();
    }

    #region Increasers
    public bool BuyItem(int myCurrencyAmount, int itemId) {
        MarketItem item = _market.MarketItems[itemId];

        int id = item.GetId();
        int price = item.GetCurrentPrice();
        bool costAffordable = AmIAbleToBuyIt(myCurrencyAmount, price);

        if (costAffordable) {
            item.Buy();
            OnBuyItem?.Invoke(id, price);
        }

        return costAffordable;
    }

    #endregion

    #region Setters

    #endregion

    #region Reporters

    public MarketItem GetMarketItem(int itemId) {
        return _market.MarketItems[itemId];
    }

    #endregion

    #region Custom Methods

    public bool AmIAbleToBuyIt(int myCurrencyAmount, int price) {
        if (myCurrencyAmount - price < 0) {
            return false;
        } else {
            return true;
        }
    }

    #endregion

}
