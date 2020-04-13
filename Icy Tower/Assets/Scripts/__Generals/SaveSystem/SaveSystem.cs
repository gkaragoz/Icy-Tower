using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void SavePlayer(PlayerStats_SO playerStats) {
        try {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.ganover";
            FileStream fileStream;
            fileStream = new FileStream(path, FileMode.OpenOrCreate);
            PlayerData playerData = new PlayerData(playerStats);
            //TODO:Encryption
            formatter.Serialize(fileStream, playerData);
            fileStream.Close();

        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    public static PlayerData LoadPlayer() {

        try {
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

        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
        return null;
    }
}
