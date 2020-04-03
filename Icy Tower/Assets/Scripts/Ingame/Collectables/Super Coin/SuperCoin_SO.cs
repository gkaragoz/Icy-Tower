using UnityEngine;

[CreateAssetMenu(fileName = "SuperCoin Stats", menuName = "Scriptable Objects/Collectables/SuperCoin Stats")]
public class SuperCoin_SO : ScriptableObject{


    [SerializeField]
    private string _name = "Super Coin";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private int amount;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public int Amount {
        get { return amount; }
        set { amount = value; }
    }
}
