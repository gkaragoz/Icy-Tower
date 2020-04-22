using TMPro;
using UnityEngine;

public class MarketItemVirtualCurrencyUI : MarketItemUIBase {

    [SerializeField]
    private TextMeshProUGUI _txtCount = null;

    public override void UpdateUI() {
        this._txtCount.text = MarketManager.instance.GetMarketItem(_marketItem.GetId()).GetVirtualCurrencyAmountOnReward().ToString();
    }

}
