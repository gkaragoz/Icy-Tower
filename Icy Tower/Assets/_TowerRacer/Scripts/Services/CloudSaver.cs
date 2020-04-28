using Library.CloudSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

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
                foreach (var data in resultCallback) {
                    Debug.Log(data);
                }
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
        PropertyInfo[] propertyInfos;
        propertyInfos = playerStats.GetType().GetProperties();

        Dictionary<string, string> playerData = new Dictionary<string, string>();

        // -2 because hideFlags and name that comes from MonoBehaviour base class.
        for (int ii = 0; ii < propertyInfos.Length - 2; ii++) {
            //if (propertyInfos[ii].PropertyType == typeof(MarketItem[])) {
            //    for (int jj = 0; jj < playerStats.MarketItems.Length; jj++) {
            //        Debug.Log(playerStats.MarketItems[jj].GetName());
            //    }
            //}

            string propertyName = propertyInfos[ii].Name;
            string propertyValue = playerStats.GetType().GetProperty(propertyName).GetValue(playerStats, null).ToString();

            playerData.Add(propertyName, propertyValue);
        }

        AddOrUpdateUserDatas(playerData);
    }

}
