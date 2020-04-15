using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [Header("Debug")]
    [Utils.ReadOnly]
    [SerializeField]
    private PlayerStats_SO _player = null;

    public void Init(PlayerStats_SO playerStats_SO) {
        this._player = playerStats_SO;
    }

    #region Increasers

    public void AddGold(int value) {
        _player.Gold += value;
    }
    
    public void AddKey(int value) {
        _player.Key += value;
    }

    public void AddGem(int value) {
        _player.Gem += value;
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

}
