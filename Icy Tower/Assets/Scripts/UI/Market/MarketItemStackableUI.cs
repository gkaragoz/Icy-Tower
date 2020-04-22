using UnityEngine;
using TMPro;

public class MarketItemStackableUI : MarketItemUIBase {

    [SerializeField]
    private TextMeshProUGUI _txtCount = null;
    [SerializeField]
    private string _prefixText = "Amount:";

    public override void UpdateUI() {
        base.UpdateUI();

        Debug.Log("Higher layer..... Set stackedAmount bro");
        this._txtCount.text = _prefixText + MarketManager.instance.GetMarketItem(_marketItem.GetId()).GetStackedAmount().ToString();
    }

}
