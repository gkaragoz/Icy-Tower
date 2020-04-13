using UnityEngine;

[System.Serializable]
public class PlayerData{

    public int playerGold = 0;
    public int highScore = 0;

    public PlayerData(PlayerStats_SO playerStats) {
        if (playerStats == null) {

            playerGold = 0;
            highScore = 0;
            return;
        }

        playerGold = playerStats.Gold;
        highScore = playerStats.HighScore;
    }
}
