using UnityEngine;

public class MarketItemPermanentUI : MarketItemUIBase {

    [SerializeField]
    private bool _hasBuyedBefore = false;

    public override void UpdateUI() {
        base.UpdateUI();

        if (_hasBuyedBefore) {
            Debug.Log("Higher layer..... Set enabled ttick permanent");
            //Show tick ui.
        } else {
            Debug.Log("Higher layer..... Set disabled ttick permanet. open buy button");
            //Show buy ui.
        }
    }

}
