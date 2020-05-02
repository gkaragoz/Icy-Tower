using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketManager : MonoBehaviour {

    #region Singleton

    public static MarketManager instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    public Action OnBuyItem;

    [SerializeField]
    private MarketItem[] _marketDB = null;
    [SerializeField]
    private List<MarketItem_SO> _tempCatalogItems = new List<MarketItem_SO>();

    public MarketItem[] MarketItems {
        get {
            return _marketDB;
        }
    }

    public void InitBy(MarketItem[] marketItems) {
        _marketDB = marketItems;

        _marketDB = _marketDB.OrderBy(i => i.GetId()).ToArray();
    }

    public void SetTempFetchedData(List<CatalogItem> marketItems) {
        _tempCatalogItems = new List<MarketItem_SO>();

        for (int ii = 0; ii < marketItems.Count; ii++) {
            MarketItem_SO marketItemSO = Newtonsoft.Json.JsonConvert.DeserializeObject<MarketItem_SO>(marketItems[ii].CustomData);
            _tempCatalogItems.Add(marketItemSO);
        }
        _tempCatalogItems = _tempCatalogItems.OrderBy(i => i.Id).ToList();
    }

    public void OverrideFetchedData() {
        for (int ii = 0; ii < _tempCatalogItems.Count; ii++) {
            _marketDB[ii].SetInitialPrice(_tempCatalogItems[ii].InitialPrice);
            _marketDB[ii].SetInflationMultiplier(_tempCatalogItems[ii].InflationMultiplier);
        }
    }

    public void ProcessBuy(int itemId) {
        MarketItem item = GetMarketItem(itemId);

        if (item.GetIsVirtualCurrency()) {
            BuyVirtualCurrencyViaRealMoney(item);

            Account.instance.Save();

            return;
        }

        BuyPermanentItemViaRealMoney(item);

        Account.instance.Save();
    }

    public void BuyItem(int itemId) {
        MarketItem item = GetMarketItem(itemId);

        if (item.GetVirtualCurrencyOnBuy() == VirtualCurrency.Real) {
            if (item.GetHasPermanentItemPurchased() == true) {
                return;
            }

            MarketService.instance.BuyItem(item.GetId());

            Account.instance.Save();

            return;
        }

        if (item.GetIsVirtualCurrency()) {
            BuyVirtualCurrency(item);

            Account.instance.Save();

            return;
        }

        if (item.GetIsLevelable()) {
            BuyLevelable(item);

            Account.instance.Save();

            return;
        }

        if (item.GetIsStackable()) {
            BuyStackable(item);

            Account.instance.Save();

            return;
        }
    }

    private void BuyVirtualCurrency(MarketItem item) {
        VirtualCurrency vcOnBuy = item.GetVirtualCurrencyOnBuy();
        VirtualCurrency vcOnReward = item.GetVirtualCurrencyOnReward();
        int rewardAmount = item.GetVirtualCurrencyAmountOnReward();

        int myMoney = Account.instance.GetCurrencyAmount(vcOnBuy);
        bool isAffordable = ExtensionMethods.AmIAbleToBuyIt(myMoney, item.GetCurrentPrice());

        // Add the virtual currency to my account.
        if (isAffordable) {
            switch (vcOnReward) {
                case VirtualCurrency.Gold:
                    Account.instance.AddVirtualCurrency(rewardAmount, VirtualCurrency.Gold);
                    break;
                case VirtualCurrency.Gem:
                    Account.instance.AddVirtualCurrency(rewardAmount, VirtualCurrency.Gem);
                    break;
                case VirtualCurrency.Key:
                    Account.instance.AddVirtualCurrency(rewardAmount, VirtualCurrency.Key);
                    break;
                default:
                    break;
            }

            Account.instance.DecreaseVirtualCurrency(item.GetCurrentPrice(), vcOnBuy);

            OnBuyItem?.Invoke();
        } else {
            // Open not enough virtual currency popup message.
            UIManager.instance.OpenPopup("OH NO!", "Not enough money!");
        }
    }
    private void BuyVirtualCurrencyViaRealMoney(MarketItem item) {
        VirtualCurrency vcOnReward = item.GetVirtualCurrencyOnReward();
        int rewardAmount = item.GetVirtualCurrencyAmountOnReward();

        // Add the virtual currency to my account.
        switch (vcOnReward) {
            case VirtualCurrency.Gold:
                Account.instance.AddVirtualCurrency(rewardAmount, VirtualCurrency.Gold);
                break;
            case VirtualCurrency.Gem:
                Account.instance.AddVirtualCurrency(rewardAmount, VirtualCurrency.Gem);
                break;
            case VirtualCurrency.Key:
                Account.instance.AddVirtualCurrency(rewardAmount, VirtualCurrency.Key);
                break;
            default:
                break;
        }

        OnBuyItem?.Invoke();
    }

    private void BuyLevelable(MarketItem item) {
        VirtualCurrency vcOnBuy = item.GetVirtualCurrencyOnBuy();

        int myMoney = Account.instance.GetCurrencyAmount(vcOnBuy);
        bool isAffordable = ExtensionMethods.AmIAbleToBuyIt(myMoney, item.GetCurrentPrice());

        // Decrease my money.
        // Increase level.
        // Save account.
        if (isAffordable) {
            Account.instance.DecreaseVirtualCurrency(item.GetCurrentPrice(), vcOnBuy);
            item.IncreaseLevel();

            OnBuyItem?.Invoke();
        } else {
            // Open not enough virtual currency popup message.
            UIManager.instance.OpenPopup("OH NO!", "Not enough money!");
        }
    }

    private void BuyStackable(MarketItem item) {
        VirtualCurrency vcOnBuy = item.GetVirtualCurrencyOnBuy();

        int myMoney = Account.instance.GetCurrencyAmount(vcOnBuy);
        bool isAffordable = ExtensionMethods.AmIAbleToBuyIt(myMoney, item.GetCurrentPrice());

        // Decrease my money.
        // Increase my stacked amount.
        // Save account.
        if (isAffordable) {
            Account.instance.DecreaseVirtualCurrency(item.GetCurrentPrice(), vcOnBuy);
            item.IncreaseStackedAmount();
            
            OnBuyItem?.Invoke();
        } else {
            // Open not enough virtual currency popup message.
            UIManager.instance.OpenPopup("OH NO!", "Not enough money!");
        }
    }

    private void BuyPermanentItemViaRealMoney(MarketItem item) {
        // Open item.
        // Save account.
        item.OpenClosePermanentItem(true);

        OnBuyItem?.Invoke();
    }

    public MarketItem GetMarketItem(int itemId) {
        return _marketDB.Where(item => item.GetId() == itemId).SingleOrDefault();
    }

}
