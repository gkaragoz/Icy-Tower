using Library.CloudSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSaver {

    // Get Single User Data 
    public static void GetSingleUserData(string key, Action<string> success, Action<string> error) {
        CloudSaveOnDataTable.GetSingleUserData(
            key,
            (resultCallback) => {
                success(resultCallback);
            },
            (errorCallback) => {
                error(errorCallback);
            });
    }

    // Get Many User Data 
    public static void GetManyUserData(List<string> keyList, Action<ArrayList> success, Action<string> error) {
        CloudSaveOnDataTable.GetManyUserData(
            keyList,
            (resultCallback) => {
                success(resultCallback);
                foreach (var data in resultCallback) {
                    Debug.Log(data);
                }
            },
            (errorCallback) => {
                error(errorCallback);
                Debug.Log(errorCallback);
            });
    }

    // Get All User Data 
    public static void GetAllUserData(Action<ArrayList> success, Action<string> error) {
        CloudSaveOnDataTable.GetAllUserData(
            (resultCallback) => {
                success(resultCallback);
            }, 
            (errorCallback) => {
                error(errorCallback);
                Debug.Log(errorCallback);
            });
    }

    // Add New User Data
    public static void AddOrUpdateUserDatas(Dictionary<string, string> data) {
        CloudSaveOnDataTable.SetUserData(data);
    }

    public static void Sync(PlayerStats_SO playerStats) {
        string playerStatsJson = JsonUtility.ToJson(playerStats);
        MarketItem_SO[] marketItemSOs = new MarketItem_SO[playerStats.MarketItems.Length];
        for (int ii = 0; ii < marketItemSOs.Length; ii++) {
            marketItemSOs[ii] = playerStats.MarketItems[ii].GetMarketItemSO();
        }

        DataRepo data = new DataRepo() {
            PlayerStatsSO = playerStats,
            MarketItemSOs = marketItemSOs
        };

        string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        AddOrUpdateUserDatas(new Dictionary<string, string>() { { "dataJson", dataJson } });
    }

}
