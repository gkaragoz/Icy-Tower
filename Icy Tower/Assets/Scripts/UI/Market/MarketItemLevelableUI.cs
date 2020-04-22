using UnityEngine;
using TMPro;

public class MarketItemLevelableUI : MarketItemUIBase {

    [SerializeField]
    private TextMeshProUGUI _txtLevel = null;

    public override void UpdateUI() {
        this._txtLevel.text = this._marketItem.GetCurrentLevel().ToString();
    }

}
