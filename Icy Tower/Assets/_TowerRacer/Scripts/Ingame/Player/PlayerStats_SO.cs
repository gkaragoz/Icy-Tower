using Newtonsoft.Json;
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
    [JsonIgnore]
    private MarketItem[] _marketItems = null;

    [SerializeField]
    private int _combo = 0;

    [SerializeField]
    private string _headGroup = "0";

    [SerializeField]
    private string _bodyGroup = "0";

    [SerializeField]
    private string _shoesGroup = "0";


    [SerializeField]
    private string _currentHead = "0,0";

    [SerializeField]
    private string _currentBodyUp = "0,1,0";

    [SerializeField]
    private string _currentBodyDown = "1,2,0";

    [SerializeField]
    private string _currentShoes = "0";

    public string CurrentHead
    {
        get { return _currentHead; }
        set { _currentHead = value; }
    }
    public string CurrentBodyUp
    {
        get { return _currentBodyUp; }
        set { _currentBodyUp = value; }
    }
    public string CurrentBodyDown
    {
        get { return _currentBodyDown; }
        set { _currentBodyDown = value; }
    }

    public string CurrentShoes  
    {
        get { return _currentShoes; }
        set { _currentShoes = value; }
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

    [JsonIgnore]
    public MarketItem[] MarketItems {
        get { return _marketItems; }
        set { _marketItems = value; }
    }

    public string HeadGroup {
        get { return _headGroup; }
        set { _headGroup = value; }
    }

    public string BodyGroup {
        get { return _bodyGroup; }
        set { _bodyGroup = value; }
    }

    public string ShoesGroup {
        get { return _shoesGroup; }
        set { _shoesGroup = value; }
    }

    public int Combo {
        get { return _combo; }
        set { _combo = value; }
    }

}