using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WardrobeItemUI : MonoBehaviour {

    [SerializeField]
    private ClothHeadMapping _clothHeadMapping = null;
    [SerializeField]
    private ClothBodyMapping _clothBodyMapping = null;
    [SerializeField]
    private ClothShoeMapping _clothShoeMapping = null;
    [SerializeField]
    private ClothType _clothType;
    [SerializeField]
    private Color _activeColor;
    [SerializeField]
    private Color _disabledColor;

    [SerializeField]
    [Utils.ReadOnly]
    private Image _imgItemIcon = null;
    [SerializeField]
    [Utils.ReadOnly]
    private TextMeshProUGUI _txtPrice = null;
    [SerializeField]
    [Utils.ReadOnly]
    private Image _imgPriceIcon = null;

    [SerializeField]
    [Utils.ReadOnly]
    private Button _btnBuy;

    [SerializeField]
    [Utils.ReadOnly]
    private Button _btnUse;

    public void UpdateUI() {
        string[] splittedStrings = null;
        bool containsMe = false;
        bool costAffordable = false;

        int myMoney = Account.instance.GetCurrencyAmount(VirtualCurrency.Gold);

        switch (_clothType) {
            case ClothType.Head:
                string headItemsString = Account.instance.GetClothItems(ClothType.Head);
                splittedStrings = headItemsString.Split(',');

                containsMe = splittedStrings.Any(idStr => idStr == _clothHeadMapping.id);
                costAffordable = ExtensionMethods.AmIAbleToBuyIt(myMoney, _clothHeadMapping.price);
                break;
            case ClothType.Body:
                string bodyItemsString = Account.instance.GetClothItems(ClothType.Body);
                splittedStrings = bodyItemsString.Split(',');

                containsMe = splittedStrings.Any(idStr => idStr == _clothBodyMapping.id);
                costAffordable = ExtensionMethods.AmIAbleToBuyIt(myMoney, _clothBodyMapping.price);
                break;
            case ClothType.Shoe:
                string shoeItemsString = Account.instance.GetClothItems(ClothType.Shoe);
                splittedStrings = shoeItemsString.Split(',');

                containsMe = splittedStrings.Any(idStr => idStr == _clothShoeMapping.id);
                costAffordable = ExtensionMethods.AmIAbleToBuyIt(myMoney, _clothShoeMapping.price);
                break;
            default:
                break;
        }

        if (containsMe) {
            SetMode(WardrobeItemUIMode.Use);
        } else {
            SetMode(WardrobeItemUIMode.Buy, costAffordable);
        }
    }

    public void Init() {
        SetObjectReferences();

        // Assign button onclicks events.
        _btnBuy.onClick.AddListener(() => {
            switch (_clothType) {
                case ClothType.Head:
                    WardrobePanelManager.instance.Buy(_clothHeadMapping, _clothType);
                    break;
                case ClothType.Body:
                    WardrobePanelManager.instance.Buy(_clothBodyMapping, _clothType);
                    break;
                case ClothType.Shoe:
                    WardrobePanelManager.instance.Buy(_clothShoeMapping, _clothType);
                    break;
                default:
                    break;
            }
        });

        _btnUse.onClick.AddListener(() => {
            switch (_clothType) {
                case ClothType.Head:
                    WardrobePanelManager.instance.Use(_clothHeadMapping, _clothType);
                    break;
                case ClothType.Body:
                    WardrobePanelManager.instance.Use(_clothBodyMapping, _clothType);
                    break;
                case ClothType.Shoe:
                    WardrobePanelManager.instance.Use(_clothShoeMapping, _clothType);
                    break;
                default:
                    break;
            }
        });
    }

    public void SetMode(WardrobeItemUIMode mode, bool costAffordable = false) {
        switch (mode) {
            case WardrobeItemUIMode.Use:
                DisableBuyUI();
                EnableUseUI();
                break;
            case WardrobeItemUIMode.Buy:
                DisableUseUI();
                EnableBuyUI(costAffordable);
                break;
            default:
                DisableUseUI();
                DisableBuyUI();
                break;
        }
    }

    private void SetObjectReferences() {
        _btnBuy = GetComponent<Button>();
        _imgItemIcon = transform.Find("imgItemIcon").GetComponent<Image>();
        _btnUse = transform.Find("btnUse").GetComponent<Button>();
        _txtPrice = transform.Find("txtPrice").GetComponent<TextMeshProUGUI>();
        _imgPriceIcon = transform.Find("imgPriceIcon").GetComponent<Image>();
    }

    private void EnableUseUI() {
        _btnUse.gameObject.SetActive(true);
    }

    private void DisableUseUI() {
        _btnUse.gameObject.SetActive(false);
    }

    private void EnableBuyUI(bool costAffordable) {
        _txtPrice.gameObject.SetActive(true);
        _imgPriceIcon.gameObject.SetActive(true);

        _btnBuy.interactable = costAffordable;
        SetAlphaOfIcon(costAffordable);
    }

    private void DisableBuyUI() {
        _btnBuy.interactable = false;
        _txtPrice.gameObject.SetActive(false);
        _imgPriceIcon.gameObject.SetActive(false);
    }

    private void SetAlphaOfIcon(bool high) {
        if (high) {
            _imgItemIcon.color = _activeColor;
        } else {
            _imgItemIcon.color = _disabledColor;
        }
    }

}
