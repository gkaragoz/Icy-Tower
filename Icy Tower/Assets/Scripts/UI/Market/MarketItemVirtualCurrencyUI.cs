using TMPro;
using UnityEngine;

public class MarketItemVirtualCurrencyUI : MarketItemUIBase {

    [SerializeField]
    private TextMeshProUGUI _txtCount = null;

    public override void UpdateUI() {
        base.UpdateUI();

        Debug.Log("Higher layer..... Set rewardCount bro");
        this._txtCount.text = MarketManager.instance.GetMarketItem(_marketItem.GetId()).GetVirtualCurrencyAmountOnReward().ToString();
    }

}
