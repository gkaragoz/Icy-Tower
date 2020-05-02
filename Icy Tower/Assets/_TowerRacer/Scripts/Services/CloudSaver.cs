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

    public static void Sync(DataRepo dataRepo) {
        string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(dataRepo);

        AddOrUpdateUserDatas(new Dictionary<string, string>() { { "Data", dataJson } });
    }

}
