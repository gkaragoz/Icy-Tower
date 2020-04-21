using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStats_SO : ScriptableObject {

    [SerializeField]
    private string _name = "Player";

    [SerializeField]
    [Utils.ReadOnly]
    private int _currentScore = 0;

    [SerializeField]
    [Utils.ReadOnly]
    private int _highScore = 0;

    [SerializeField]
    [Utils.ReadOnly]
    private int _gold = 0;

    [SerializeField]
    [Utils.ReadOnly]
    private int _key = 0;

    [SerializeField]
    [Utils.ReadOnly]
    private int _gem = 0;

    [SerializeField]
    private Item[] _items = null;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public int CurrentScore {
        get { return _currentScore; }
        set { _currentScore = value; }
    }

    public int HighScore {
        get { return _highScore; }
        set { _highScore = value; }
    }

    public int Gold {
        get { return _gold; }
        set { _gold = value; }
    }

    public int Key {
        get { return _key; }
        set { _key = value; }
    }

    public int Gem {
        get { return _gem; }
        set { _gem = value; }
    }

    public Item[] Items {
        get { return _items; }
        set { _items = value; }
    }

}

[System.Serializable]
public class Item {
    public MarketItem_SO marketItemSO;
    public int count;
}