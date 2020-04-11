using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SavePlayer(PlayerStats_SO playerStats) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.ganover";
        FileStream fileStream;
        if (File.Exists(path)) {
            fileStream = new FileStream(path, FileMode.Open);
        } else {
            fileStream = new FileStream(path, FileMode.Create);
        }

        PlayerData playerData = new PlayerData(playerStats);
        //TODO:Encryption
        formatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public static PlayerData LoadPlayer() {
        Debug.Log("player loaded");
        string path = Application.persistentDataPath + "/player.ganover";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            //TODO:Decryption
            PlayerData playerData = formatter.Deserialize(fileStream) as PlayerData;
            fileStream.Close();
            return playerData;

        } else {
            Debug.Log("file does not exist in" + path);
            return null;
        }
    }
}
