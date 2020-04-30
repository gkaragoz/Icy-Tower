using Library.Purchasing;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MarketService : MonoBehaviour {

    #region Singleton

    public static MarketService instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _androidIAP = new AndroidIAP();

        _androidIAP.OnIAPServicesInitialized += OnServiceInitializeSucceed;

        _androidIAP.OnIAPServicesInitializeFailed += OnServiceInitializeFailed;

        _androidIAP.OnPurchasesValidationSucceed += OnValidationSucceed;

        _androidIAP.OnPurchasesValidationFailed += OnValidationFailed;

        _androidIAP.OnPurchasesSucceed += OnPurchaseSucceed;

        _androidIAP.OnPurchasesFailed += OnPurchaseFailed;
    }

    #endregion

    private AndroidIAP _androidIAP;

    public void Fetch(Action<List<CatalogItem>> items, Action<string> errorMessage) {
        _androidIAP.InitializeIAPItems(
            (actionSuccess) => {
                // actionSuccess -> LIST OF YOUR MARKET ITEMS

                // YOU CAN INITIALIZE PURCHASING
                _androidIAP.InitializePurchasingServices(AndroidIAP.ItemType.Mixed);

                items(actionSuccess);
            },

            (actionFailure) => {
                // SOMETHING WRONG, YOU CAN'T HANDLE ANY PURHCASING PROCESS

                errorMessage(actionFailure);
            });
    }

    private void OnServiceInitializeSucceed() {
        // SERVICES ARE READY TO PURCHASE PROCESS

        //_androidIAP.BuyProduct("PRODUCT_WEAPON");
    }

    private void OnServiceInitializeFailed(string error) {
        Debug.LogError("OnServiceInitializeFailed! : " + error);
    }

    private void OnValidationSucceed() {
        Debug.LogError("OnValidationSucceed!");
    }

    private void OnValidationFailed(string error) {
        Debug.LogError("OnValidationFailed! : " + error);
    }

    private void OnPurchaseSucceed() {
        Debug.LogError("OnPurchaseSucceed!");
    }

    private void OnPurchaseFailed(string error) {
        Debug.LogError("OnPurchaseFailed! : " + error);
    }

}
