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

    public void AddCombo() {
        _player.Combo++;
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

    public void SetCombo(int value) {
        _player.Combo = value;
    }

    public void SetCurrentShoes(string data)
    {
        _player.CurrentShoes = data;
    }
    public void SetCurrentHead(string data)
    {
        _player.CurrentHead = data;
    }
    public void SetCurrentBodyUp(string data)
    {
        _player.CurrentBodyUp = data;
    }

    public void SetCurrentBodyDown(string data)
    {
        _player.CurrentBodyDown = data;
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

    public int GetCombo() {
        return _player.Combo;
    }

    public string GetClothItems(ClothType clothType) {
        switch (clothType) {
            case ClothType.Head:
                return _player.HeadGroup;
            case ClothType.Body:
                return _player.BodyGroup;
            case ClothType.Shoe:
                return _player.ShoesGroup;
            default:
                return string.Empty;
        }
    }


    public string GetCurrentBodyUp()
    {
        return _player.CurrentBodyUp;
    }
    public string GetCurrentBodyDown()
    {
        return _player.CurrentBodyDown;
    }
    public string GetCurrentHead()
    {
        return _player.CurrentHead;
    }
    public string GetCurrentShoes()
    {
        return _player.CurrentShoes;
    }


    #endregion

    #region Add

    public void AddClothItem(ClothType clothType, string itemId) {
        switch (clothType) {
            case ClothType.Head:
                _player.HeadGroup += "," + itemId; 
                break;
            case ClothType.Body:
                _player.BodyGroup += "," + itemId;
                break;
            case ClothType.Shoe:
                _player.ShoesGroup += "," + itemId; 
                break;
            default:
                break;
        }
    }

    #endregion

}
