using Library.CloudSave;
using System.Collections.Generic;
using UnityEngine;

public class CloudSaveExample : MonoBehaviour
{
    /*******************************************************************************************************************/

    public void GetSingleUserData() // Get Single User Data 
    {
        CloudSaveOnDataTable.GetSingleUserData("coin",
            (result) => {
                Debug.Log(result);
            },
            (errorMessage) => {
                Debug.Log(errorMessage);
            });
    }

    /*******************************************************************************************************************/

    public void GetManyUserData() // Get Many User Data 
    {
        List<string> keyList = new List<string>() { "health", "coin", "gem" };

        CloudSaveOnDataTable.GetManyUserData(keyList,
            (result) => {
                foreach (var data in result) {
                    Debug.Log(data);
                }
            },
            (errorMessage) => {
                Debug.Log(errorMessage);
            });
    }

    /*******************************************************************************************************************/

    public void GetAllUserData() // Get All User Data 
    {
        CloudSaveOnDataTable.GetAllUserData(
            (result) => {
                foreach (var data in result) {
                    Debug.Log(data);
                }
            },
            (errorMessage) => {
                Debug.Log(errorMessage);
            });
    }

    /*******************************************************************************************************************/

    public void RemoveUserData() // Remove User Data
    {
        CloudSaveOnDataTable.RemoveUserData(new List<string>() { "coin" });
    }

    /*******************************************************************************************************************/

    public void AddUserNewData() // Add New User Data
    {
        Dictionary<string, string> datas = new Dictionary<string, string>();

        datas.Add("floor","5");

        CloudSaveOnDataTable.SetUserData(datas);
    }

    /*******************************************************************************************************************/

    private void Update()
    {
        if (Input.GetKey("w")) // Get Single User Data
        {
            GetSingleUserData();
        }

        else if (Input.GetKey("s")) // Get Many User Data
        {
            GetManyUserData();
        }

        else if (Input.GetKey("d")) // Get All Data
        {
            GetAllUserData();
        }

        else if (Input.GetKey("a")) // Remove User Data
        {
            RemoveUserData();
        }

        else if (Input.GetKey("e")) // Add User New Data
        {
            AddUserNewData();
        }
    }
}
