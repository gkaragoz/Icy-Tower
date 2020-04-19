using UnityEngine;

[CreateAssetMenu(fileName = "Market", menuName = "Scriptable Objects/Market")]
public class Market_SO : ScriptableObject {

    [SerializeField]
    private MarketItem[] _marketItems = null;

    public MarketItem[] MarketItems {
        get {
            return _marketItems;
        }
        set {
            _marketItems = value;
        }
    }
    
}