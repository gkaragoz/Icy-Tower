using System;
using UnityEngine;

public class Market : MonoBehaviour {

    public Action<int, int> OnBuyItem;

    [Header("Debug")]
    [Utils.ReadOnly]
    [SerializeField]
    private Market_SO _market = null;

    public void Init(Market_SO marketSO) {
        this._market = marketSO;
    }

    #region Increasers
    public void BuyItem(int myCurrencyAmount, int itemId) {
        MarketItem item = _market.MarketItems[itemId];

        int id = item.GetId();
        int price = item.GetCurrentPrice();
        bool costAffordable = AmIAbleToBuyIt(myCurrencyAmount, price);

        if (costAffordable) {
            item.Buy();
            OnBuyItem?.Invoke(id, price);
        } else {
            OnBuyItem?.Invoke(-1, -1);
        }
    }

    #endregion

    #region Setters

    #endregion

    #region Reporters


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
