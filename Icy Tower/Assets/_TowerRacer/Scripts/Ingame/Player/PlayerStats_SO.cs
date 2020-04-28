using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Player Stats")]
public class PlayerStats_SO : ScriptableObject {

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
    private MarketItem[] _marketItems = null;

    [SerializeField]
    private string _headGroup = "0";

    [SerializeField]
    private string _bodyGroup = "0";

    [SerializeField]
    private string _shoesGroup = "0";

    [SerializeField]
    private int _combo = 0;

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

    public MarketItem[] MarketItems {
        get { return _marketItems; }
        set { _marketItems = value; }
    }

    [SerializeField]
    public string HeadGroup {
        get { return _headGroup; }
        set { _headGroup = value; }
    }

    [SerializeField]
    public string BodyGroup {
        get { return _bodyGroup; }
        set { _bodyGroup = value; }
    }

    [SerializeField]
    public string ShoesGroup {
        get { return _shoesGroup; }
        set { _shoesGroup = value; }
    }

    public int Combo {
        get { return _combo; }
        set { _combo = value; }
    }

}