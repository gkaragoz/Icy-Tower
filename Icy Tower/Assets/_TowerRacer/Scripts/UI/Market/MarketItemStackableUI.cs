using UnityEngine;
using TMPro;

public class MarketItemStackableUI : MarketItemUIBase {

    [SerializeField]
    private TextMeshProUGUI _txtCount = null;
    [SerializeField]
    private string _prefixText = "Amount:";

    public override void UpdateUI() {
        base.UpdateUI();

        this._txtCount.text = _prefixText + MarketManager.instance.GetMarketItem(_marketItem.GetId()).GetStackedAmount().ToString();
    }

}
