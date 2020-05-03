using UnityEngine;

public class MarketItemPermanentUI : MarketItemUIBase {

    [SerializeField]
    private bool _hasBuyedBefore = false;

    public override void UpdateUI() {
        base.UpdateUI();

        if (_hasBuyedBefore) {
            //Show tick ui.
            _btnBuy.interactable = false;
            _txtPrice.SetText("Owned!");
        }
    }

}