using UnityEngine;
using TMPro;

public class MarketItemStackableUI : MarketItemUIBase {

    [SerializeField]
    private TextMeshProUGUI _txtCount = null;

    public override void UpdateUI() {
        this._txtCount.text = MarketManager.instance.GetMarketItem(_marketItem.GetId()).GetStackedAmount().ToString();
    }

}
