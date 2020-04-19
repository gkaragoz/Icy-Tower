using UnityEngine;

[CreateAssetMenu(fileName = "Market Item", menuName = "Scriptable Objects/Market Item")]
public class MarketItem_SO : ScriptableObject {

    [SerializeField]
    private int _id;
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _currentLevel;
    [SerializeField]
    private int _currentPrice;
    [SerializeField]
    private VirtualCurrency _currencyType;
    [SerializeField]
    private bool _isLevelable = false;
    [SerializeField]
    private bool _isInflationable = false;

    public int Id {
        get { return _id; }
        set { _id = value; }
    }

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public int CurrentPrice {
        get { return _currentPrice; }
        set { _currentPrice = value; }
    }

    public int CurrentLevel {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    public VirtualCurrency Currency {
        get { return _currencyType; }
        set { _currencyType = value; }
    }

    public bool IsLeveable {
        get { return _isLevelable; }
        set { _isLevelable = value; }
    }

    public bool IsInflationable {
        get { return _isInflationable; }
        set { _isInflationable = value; }
    }

}
