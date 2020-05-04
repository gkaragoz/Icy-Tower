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

    [JsonProperty(PropertyName = "1")]
    public string CurrentHead {
        get { return _currentHead; }
        set { _currentHead = value; }
    }
    [JsonProperty(PropertyName = "2")]
    public string CurrentBodyUp {
        get { return _currentBodyUp; }
        set { _currentBodyUp = value; }
    }
    [JsonProperty(PropertyName = "3")]
    public string CurrentBodyDown {
        get { return _currentBodyDown; }
        set { _currentBodyDown = value; }
    }

    [JsonProperty(PropertyName = "4")]
    public string CurrentShoes {
        get { return _currentShoes; }
        set { _currentShoes = value; }
    }

    [JsonProperty(PropertyName = "5")]
    public int CurrentScore {
        get { return _currentScore; }
        set { _currentScore = value; }
    }

    [JsonProperty(PropertyName = "6")]
    public int HighScore {
        get { return _highScore; }
        set { _highScore = value; }
    }

    [JsonProperty(PropertyName = "7")]
    public int Gold {
        get { return _gold; }
        set { _gold = value; }
    }

    [JsonProperty(PropertyName = "8")]
    public int Key {
        get { return _key; }
        set { _key = value; }
    }

    [JsonProperty(PropertyName = "9")]
    public int Gem {
        get { return _gem; }
        set { _gem = value; }
    }

    [JsonIgnore]
    public MarketItem[] MarketItems {
        get { return _marketItems; }
        set { _marketItems = value; }
    }

    [JsonProperty(PropertyName = "10")]
    public string HeadGroup {
        get { return _headGroup; }
        set { _headGroup = value; }
    }

    [JsonProperty(PropertyName = "11")]
    public string BodyGroup {
        get { return _bodyGroup; }
        set { _bodyGroup = value; }
    }

    [JsonProperty(PropertyName = "12")]
    public string ShoesGroup {
        get { return _shoesGroup; }
        set { _shoesGroup = value; }
    }

    [JsonProperty(PropertyName = "13")]
    public int Combo {
        get { return _combo; }
        set { _combo = value; }
    }

}