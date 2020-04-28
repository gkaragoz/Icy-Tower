﻿using System;
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

    public MarketItem[] MarketItems {
        get {
            return _marketDB;
        }
    }

    public void Init(MarketItem[] marketItems) {
        _marketDB = marketItems;
    }

    public void BuyItem(int itemId) {
        MarketItem item = GetMarketItem(itemId);

        if (item.GetIsVirtualCurrency()) {
            BuyVirtualCurrency(item);

            return;
        }

        if (item.GetIsLevelable()) {
            BuyLevelable(item);

            return;
        }

        if (item.GetIsStackable()) {
            BuyStackable(item);

            return;
        }

        if (item.GetHasPermanentItemPurchased() == false) {
            BuyPermanentItem(item);

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

            OnBuyItem?.Invoke();
        } else {
            // Open not enough virtual currency popup message.
            UIManager.instance.OpenPopup("OH NO!", "Not enough money!");
        }
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

            Account.instance.Save();

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

            Account.instance.Save();

            OnBuyItem?.Invoke();
        } else {
            // Open not enough virtual currency popup message.
            UIManager.instance.OpenPopup("OH NO!", "Not enough money!");
        }
    }

    private void BuyPermanentItem(MarketItem item) {
        VirtualCurrency vcOnBuy = item.GetVirtualCurrencyOnBuy();

        int myMoney = Account.instance.GetCurrencyAmount(vcOnBuy);
        bool isAffordable = ExtensionMethods.AmIAbleToBuyIt(myMoney, item.GetCurrentPrice());

        // Decrease my money.
        // Open item.
        // Save account.
        if (isAffordable) {
            Account.instance.DecreaseVirtualCurrency(item.GetCurrentPrice(), vcOnBuy);
            item.OpenClosePermanentItem(true);

            Account.instance.Save();

            OnBuyItem?.Invoke();
        } else {
            // Open not enough virtual currency popup message.
            UIManager.instance.OpenPopup("OH NO!", "Not enough money!");
        }
    }

    public MarketItem GetMarketItem(int itemId) {
        return _marketDB.Where(item => item.GetId() == itemId).SingleOrDefault();
    }

}