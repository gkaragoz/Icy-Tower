using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[CreateAssetMenu(fileName = "Market Item", menuName = "Scriptable Objects/Market Item")]
public class MarketItem_SO : ScriptableObject {

    [SerializeField]
    private int _id;
    [SerializeField]
    private string _name;
    [SerializeField]
    private bool _isVirtualCurrency = false;
    [SerializeField]
    [JsonConverter(typeof(StringEnumConverter))]
    private VirtualCurrency _virtualCurrencyOnReward;
    [SerializeField]
    private int _virtualCurrencyAmountOnReward;

    [SerializeField]
    private float _inflationMultiplier = 1f;
    [SerializeField]
    private float _initialPrice;
    [SerializeField]
    private int _currentLevel;
    [SerializeField]
    private float _currentPrice;
    [SerializeField]
    [JsonConverter(typeof(StringEnumConverter))]
    private VirtualCurrency _virtualCurrencyOnBuy;
    [SerializeField]
    private bool _isLevelable = false;
    [SerializeField]
    private bool _isInflationable = false;

    [SerializeField]
    private bool _isStackable = false;
    [SerializeField]
    private int _stackedAmount;

    [SerializeField]
    private bool _hasPermanentItemPurchased = false;

    [JsonProperty(PropertyName = "1")]
    public int Id {
        get { return _id; }
        set { _id = value; }
    }

    [JsonProperty(PropertyName = "2")]
    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    [JsonProperty(PropertyName = "3")]
    public bool IsVirtualCurrency {
        get { return _isVirtualCurrency; }
        set { _isVirtualCurrency = value; }
    }

    [JsonProperty(PropertyName = "4")]
    public VirtualCurrency VirtualCurrencyOnReward {
        get { return _virtualCurrencyOnReward; }
        set { _virtualCurrencyOnReward = value; }
    }

    [JsonProperty(PropertyName = "5")]
    public int VirtualCurrencyAmountOnReward {
        get { return _virtualCurrencyAmountOnReward; }
        set { _virtualCurrencyAmountOnReward = value; }
    }

    [JsonProperty(PropertyName = "6")]
    public float InflationMultiplier {
        get { return _inflationMultiplier; }
        set { _inflationMultiplier = value; }
    }

    [JsonProperty(PropertyName = "7")]
    public int CurrentLevel {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    [JsonProperty(PropertyName = "8")]
    public float InitialPrice {
        get { return _initialPrice; }
        set { _initialPrice = value; }
    }

    [JsonProperty(PropertyName = "9")]
    public float CurrentPrice {
        get { return _currentPrice; }
        set { _currentPrice = value; }
    }

    [JsonProperty(PropertyName = "10")]
    public VirtualCurrency VirtualCurrencyOnBuy {
        get { return _virtualCurrencyOnBuy; }
        set { _virtualCurrencyOnBuy = value; }
    }

    [JsonProperty(PropertyName = "11")]
    public bool IsLevelable {
        get { return _isLevelable; }
        set { _isLevelable = value; }
    }

    [JsonProperty(PropertyName = "12")]
    public bool IsInflationable {
        get { return _isInflationable; }
        set { _isInflationable = value; }
    }

    [JsonProperty(PropertyName = "13")]
    public bool IsStackable {
        get { return _isStackable; }
        set { _isStackable = value; }
    }

    [JsonProperty(PropertyName = "14")]
    public int StackedAmount {
        get { return _stackedAmount; }
        set { _stackedAmount = value; }
    }

    [JsonProperty(PropertyName = "15")]
    public bool HasPermanentItemPurchased {
        get { return _hasPermanentItemPurchased; }
        set { _hasPermanentItemPurchased = value; }
    }


}
