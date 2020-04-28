using UnityEngine;
using TMPro;

public class MarketItemLevelableUI : MarketItemUIBase {

    [SerializeField]
    private TextMeshProUGUI _txtLevel = null;
    [SerializeField]
    private string _prefixText = "Level:";

    public override void UpdateUI() {
        base.UpdateUI();
        
        this._txtLevel.text = _prefixText + this._marketItem.GetCurrentLevel().ToString();
    }

}
