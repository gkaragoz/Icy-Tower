using UnityEngine;

public class PlayerStats : MonoBehaviour{

    [Header("Initialization")]
    [SerializeField]
    private PlayerStats_SO _playerDefinition_Template = null;

    [Header("Debug")]
    [Utils.ReadOnly]
    [SerializeField]
    private PlayerStats_SO _player = null;

    #region Initializations

    private void Awake() {
        if (_playerDefinition_Template != null) {
            _player = Instantiate(_playerDefinition_Template);
        }
    }

    #endregion

    #region Increasers

    public void AddCurrentScore(int value) {
        _player.CurrentScore += value;

        if (_player.CurrentScore >= _player.HighScore) {
            SetHighScore(_player.CurrentScore);
        }
    }

    public void AddGold(int value) {
        _player.Gold += value;
         SavePlayer();
    }
    
    public void AddKey(int value) {
        _player.Gold += value;
        SavePlayer();
    }

    public void AddGem(int value) {
        _player.Gem += value;
        SavePlayer();
    }

    #endregion

    #region Setters

    public void SetCurrentScore(int value) {
        _player.CurrentScore = value;

        if (_player.CurrentScore >= _player.HighScore) {
            SetHighScore(_player.CurrentScore);
        }
    }

    public void SetHighScore(int value) {
        _player.HighScore = value;
        SavePlayer();
    }

    public void SetGold(int value) {
        _player.Gold = value;
    }

    public void SetKey(int value) {
        _player.Key = value;
    }

    public void SetGem(int value) {
        _player.Gem = value;
    }

    #endregion

    #region Reporters

    public string GetName() {
        return _player.Name;
    }

    public int GetCurrentScore() {
        return _player.CurrentScore;
    }

    public int GetHighScore() {
        return _player.HighScore;
    }

    public int GetGold() {
        return _player.Gold;
    }

    public int GetKey() {
        return _player.Key;
    }

    public int GetGem() {
        return _player.Gem;
    }

    #endregion

    public void SavePlayer() {
        SaveSystem.SavePlayer(_player);
    }

    public void LoadData() {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData == null)
            return;
        SetHighScore(playerData.highScore);
        SetGold(playerData.playerGold);
    }

}
