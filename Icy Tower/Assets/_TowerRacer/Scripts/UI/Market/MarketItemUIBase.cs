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
        SetMarketItem();

        _btnBuy.onClick.AddListener(() => {
            MarketManager.instance.BuyItem(_marketItem.GetId());
        });
    }

    private void SetMarketItem() {
        this._marketItem = MarketManager.instance.GetMarketItem(_marketItem.GetId());
        this._marketItem.OnMarketItemUpdated += UpdateUI;

        UpdateUI();
    }

    public virtual void UpdateUI() {
        this._txtPrice.text = this._marketItem.GetCurrentPrice().ToString();
        this._txtName.text = this._marketItem.GetName().ToString();
    }

}
