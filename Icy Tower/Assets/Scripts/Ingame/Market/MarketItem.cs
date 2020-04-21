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

    public void SetCurrentPrice(int price) {
        this._marketItem.CurrentPrice = price;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetCurrentLevel(int level) {
        this._marketItem.CurrentLevel = level;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetCurrencyType(VirtualCurrency currencyType) {
        this._marketItem.Currency = currencyType;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetIsInflationable(bool value) {
        this._marketItem.IsInflationable = value;

        OnMarketItemUpdated?.Invoke();
    }

    public void SetIsLevelable(bool value) {
        this._marketItem.IsLeveable = value;

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

    public int GetCurrentPrice() {
        return this._marketItem.CurrentPrice;
    }

    public int GetCurrentLevel() {
        return this._marketItem.CurrentLevel;
    }

    public VirtualCurrency GetCurrencyType() {
        return this._marketItem.Currency;
    }
    public bool GetIsInflationable() {
        return this._marketItem.IsInflationable;
    }

    public bool GetIsLevelable() {
        return this._marketItem.IsLeveable;
    }

    #endregion

    #region Custom Methods

    private int CalculatePrice(int level) {
        int currentPrice = this._marketItem.CurrentPrice;

        if (level == 0) {
            return currentPrice * 2;
        } else {
            return currentPrice * level;
        }
    }

    public void Buy() {
        int currentLevel = GetCurrentLevel();
        int currentPrice = GetCurrentPrice();

        int newLevel = currentLevel++;
        int newPrice = currentPrice;

        if (GetIsLevelable() && GetIsInflationable()) {
            SetCurrentLevel(newLevel);
            newPrice = CalculatePrice(newLevel);
        }

        SetCurrentPrice(newPrice);

        OnMarketItemUpdated?.Invoke();
    }

    #endregion

}
