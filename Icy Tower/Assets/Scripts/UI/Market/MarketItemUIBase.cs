using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarketItemUIBase : MonoBehaviour {

    [SerializeField]
    protected int _itemId = -1;
    [SerializeField]
    private Button _btnBuy = null;
    [SerializeField]
    private TextMeshProUGUI _txtPrice = null;
    [SerializeField]
    private TextMeshProUGUI _txtName = null;

    [Utils.ReadOnly]
    [SerializeField]
    protected MarketItem _marketItem = null;

    private void Start() {
        Market.instance.OnMarketInitialized += OnMarketInitialized;
    }

    private void OnMarketInitialized() {
        SetMarketItem(Market.instance.GetMarketItem(_itemId));

        _btnBuy.onClick.AddListener(() => {
            VirtualCurrency vc = _marketItem.GetCurrencyType();
            int currentMoney = Account.instance.GetCurrencyAmount(vc);

            bool isSuccess = Market.instance.BuyItem(currentMoney, _itemId);
            if (isSuccess) {
                Account.instance.AddItem(_itemId, true);
            } else {
                // todo, popup not enough gold.
            }
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
