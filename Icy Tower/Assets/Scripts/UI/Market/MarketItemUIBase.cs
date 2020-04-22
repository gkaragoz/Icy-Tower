using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarketItemUIBase : MonoBehaviour {

    [SerializeField]
    protected MarketItem _marketItem = null;
    [SerializeField]
    private Button _btnBuy = null;
    [SerializeField]
    private TextMeshProUGUI _txtPrice = null;
    [SerializeField]
    private TextMeshProUGUI _txtName = null;

    private void Start() {
        _btnBuy.onClick.AddListener(() => {
            MarketManager.instance.BuyItem(_marketItem.GetId());
        });
    }

    public virtual void UpdateUI() {
        this._txtPrice.text = this._marketItem.GetCurrentPrice().ToString();
        this._txtName.text = this._marketItem.GetName().ToString();
    }

    public virtual void SetMarketItem(MarketItem marketItem) {
        this._marketItem = marketItem;
        this._marketItem.OnMarketItemUpdated += UpdateUI;

        UpdateUI();
    }

}
