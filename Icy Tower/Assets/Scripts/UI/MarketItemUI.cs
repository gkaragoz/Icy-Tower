using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketItemUI : MonoBehaviour {

    [SerializeField]
    private int _itemId = -1;
    [SerializeField]
    private Button _btnBuy = null;
    [SerializeField]
    private TextMeshProUGUI _txtPrice = null;
    [SerializeField]
    private TextMeshProUGUI _txtName = null;
    [SerializeField]
    private TextMeshProUGUI _txtLevel = null;
    [SerializeField]
    private TextMeshProUGUI _txtCount = null;
    [SerializeField]
    private TextMeshProUGUI _txtUnit = null;

    [Utils.ReadOnly]
    [SerializeField]
    private MarketItem _marketItem = null;

    private void Start() {
        Market.instance.OnMarketInitialized += OnMarketInitialized;
    }

    public void OnMarketInitialized() {
        // set button on click

        SetMarketItem(Market.instance.GetMarketItem(_itemId));
    }

    public void SetMarketItem(MarketItem marketItem) {
        this._marketItem = marketItem;
        this._marketItem.OnMarketItemUpdated += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI() {
        this._txtPrice.text = this._marketItem.GetCurrentPrice().ToString();
        this._txtName.text = this._marketItem.GetName();
        this._txtLevel.text = this._marketItem.GetCurrentLevel().ToString();
    }

}
