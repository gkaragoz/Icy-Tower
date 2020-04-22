using UnityEngine;

public class MarketItemPermanentUI : MarketItemUIBase {

    [SerializeField]
    private bool _hasBuyedBefore = false;

    public override void UpdateUI() {
        if (_hasBuyedBefore) {
            //Show tick ui.
        } else {
            //Show buy ui.
        }
    }

}
