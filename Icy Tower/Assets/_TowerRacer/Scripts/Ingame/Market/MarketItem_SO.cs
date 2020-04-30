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
    private int _initialPrice;
    [SerializeField]
    private int _currentLevel;
    [SerializeField]
    private int _currentPrice;
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

    public int Id {
        get { return _id; }
        set { _id = value; }
    }

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public bool IsVirtualCurrency {
        get { return _isVirtualCurrency; }
        set { _isVirtualCurrency = value; }
    }

    public VirtualCurrency VirtualCurrencyOnReward {
        get { return _virtualCurrencyOnReward; }
        set { _virtualCurrencyOnReward = value; }
    }

    public int VirtualCurrencyAmountOnReward {
        get { return _virtualCurrencyAmountOnReward; }
        set { _virtualCurrencyAmountOnReward = value; }
    }

    public float InflationMultiplier {
        get { return _inflationMultiplier; }
        set { _inflationMultiplier = value; }
    }

    public int CurrentLevel {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    public int InitialPrice {
        get { return _initialPrice; }
        set { _initialPrice = value; }
    }

    public int CurrentPrice {
        get { return _currentPrice; }
        set { _currentPrice = value; }
    }

    public VirtualCurrency VirtualCurrencyOnBuy {
        get { return _virtualCurrencyOnBuy; }
        set { _virtualCurrencyOnBuy = value; }
    }

    public bool IsLevelable {
        get { return _isLevelable; }
        set { _isLevelable = value; }
    }

    public bool IsInflationable {
        get { return _isInflationable; }
        set { _isInflationable = value; }
    }

    public bool IsStackable {
        get { return _isStackable; }
        set { _isStackable = value; }
    }

    public int StackedAmount {
        get { return _stackedAmount; }
        set { _stackedAmount = value; }
    }

    public bool HasPermanentItemPurchased {
        get { return _hasPermanentItemPurchased; }
        set { _hasPermanentItemPurchased = value; }
    }


}
