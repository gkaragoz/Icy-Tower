using System;
using UnityEngine;

[System.Serializable]
public class MarketItem {

    public Action OnMarketItemUpdated;

    [SerializeField]
    private MarketItem_SO _marketItem = null;

    public void Init(MarketItem_SO marketItem_SO) {
        this._marketItem = marketItem_SO;
    }

    #region Setters

    public void SetId(int id) {
        this._marketItem.Id = id;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetName(string name) {
        this._marketItem.Name = name;
        
        OnMarketItemUpdated?.Invoke();
    }

    public void SetIsVirtualCurrency(bool value) {
        this._marketItem.IsVirtualCurrency = value;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetVirtualCurrencyOnReward(VirtualCurrency vc) {
        this._marketItem.VirtualCurrencyOnReward = vc;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetVirtualCurrencyAmountOnReward(int value) {
        this._marketItem.VirtualCurrencyAmountOnReward = value;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetCurrentLevel(int level) {
        this._marketItem.CurrentLevel = level;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetCurrentPrice(int price) {
        this._marketItem.CurrentPrice = price;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetVirtualCurrencyOnBuy(VirtualCurrency currencyType) {
        this._marketItem.VirtualCurrencyOnBuy = currencyType;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetIsLevelable(bool value) {
        this._marketItem.IsLevelable = value;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetIsInflationable(bool value) {
        this._marketItem.IsInflationable = value;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetIsStackable(bool value) {
        this._marketItem.IsStackable = value;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetStackedAmount(int value) {
        this._marketItem.StackedAmount = value;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetHasPermanentItemPurchased(bool value) {
        this._marketItem.HasPermanentItemPurchased = value;

        OnMarketItemUpdated?.Invoke();
    }

    #endregion

    #region Getters
    public int GetId() {
        return this._marketItem.Id;
    }

    public string GetName() {
        return this._marketItem.Name;
    }

    public bool GetIsVirtualCurrency() {
        return this._marketItem.IsVirtualCurrency;
    }

    public VirtualCurrency GetVirtualCurrencyOnReward() {
        return this._marketItem.VirtualCurrencyOnReward;
    }

    public int GetVirtualCurrencyAmountOnReward() {
        return this._marketItem.VirtualCurrencyAmountOnReward;
    }
    public int GetCurrentLevel() {
        return this._marketItem.CurrentLevel;
    }

    public int GetCurrentPrice() {
        return this._marketItem.CurrentPrice;
    }

    public VirtualCurrency GetVirtualCurrencyOnBuy() {
        return this._marketItem.VirtualCurrencyOnBuy;
    }
    public bool GetIsLevelable() {
        return this._marketItem.IsLevelable;
    }

    public bool GetIsInflationable() {
        return this._marketItem.IsInflationable;
    }

    public bool GetIsStackable() {
        return this._marketItem.IsStackable;
    }

    public int GetStackedAmount() {
        return this._marketItem.StackedAmount;
    }

    public bool GetHasPermanentItemPurchased() {
        return this._marketItem.HasPermanentItemPurchased;
    }


    #endregion

    #region Custom Methods

    private int CalculatePriceByLevel(int level) {
        int currentPrice = this._marketItem.CurrentPrice;

        if (level == 0) {
            return currentPrice * 2;
        } else {
            return currentPrice * level;
        }
    }
    private int CalculatePriceByStackedAmount(int stackedAmount) {
        int currentPrice = this._marketItem.CurrentPrice;

        if (stackedAmount == 0) {
            return currentPrice * 2;
        } else {
            return currentPrice * stackedAmount;
        }
    }

    public void IncreaseLevel() {
        int currentLevel = GetCurrentLevel();
        int currentPrice = GetCurrentPrice();

        int newLevel = currentLevel++;
        SetCurrentLevel(newLevel);

        if (GetIsInflationable()) {
            int newPrice = CalculatePriceByLevel(newLevel);
            SetCurrentPrice(newPrice);
        }

        OnMarketItemUpdated?.Invoke();
    }

    public void IncreaseStackedAmount() {
        int currentStackedAmount = GetStackedAmount();
        int newStackedAmount = currentStackedAmount++;
        SetStackedAmount(newStackedAmount);

        if (GetIsInflationable()) {
            int newPrice = CalculatePriceByStackedAmount(newStackedAmount);
            SetCurrentPrice(newPrice);
        }

        OnMarketItemUpdated?.Invoke();
    }

    public void OpenClosePermanentItem(bool value) {
        SetHasPermanentItemPurchased(value);

        OnMarketItemUpdated?.Invoke();
    }

    #endregion

}
