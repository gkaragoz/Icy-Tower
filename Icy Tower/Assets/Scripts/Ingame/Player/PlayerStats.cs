using System.Linq;
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


    #region Decreasers

    public void DecreaseGold(int value) {
        _player.Gold -= value;

        if (_player.Gold < 0) {
            _player.Gold = 0;
            Debug.LogWarning("Something got wrong in VC calculation.");
        }
    }

    public void DecreaseKey(int value) {
        _player.Key -= value;
        
        if (_player.Key < 0) {
            _player.Key = 0;
            Debug.LogWarning("Something got wrong in VC calculation.");
        }
    }

    public void DecreaseGem(int value) {
        _player.Gem -= value;

        if (_player.Gem < 0) {
            _player.Gem = 0;
            Debug.LogWarning("Something got wrong in VC calculation.");
        }
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

    public void SetItems(MarketItem[] items) {
        _player.MarketItems = items;
    }

    public void SetHeadGroup(string data) {
        _player.HeadGroup = data;
    }

    public void SetBodyGroup(string data) {
        _player.BodyGroup = data;
    }

    public void SetShoesGroup(string data) {
        _player.ShoesGroup = data;
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

    public MarketItem[] GetItems() {
        return _player.MarketItems;
    }

    public string GetHeadGroup() {
        return _player.HeadGroup;
    }

    public string GetBodyGroup() {
        return _player.BodyGroup;
    }

    public string GetShoesGroup() {
        return _player.ShoesGroup;
    }

    #endregion

}
