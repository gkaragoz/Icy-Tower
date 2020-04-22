using TMPro;
using UnityEngine;

public class MarketItemVirtualCurrencyUI : MarketItemUIBase {

    [SerializeField]
    private TextMeshProUGUI _txtCount = null;

    public override void UpdateUI() {
        this._txtCount.text = Account.instance.GetItemById(_itemId).count.ToString();
    }

}
