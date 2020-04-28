using UnityEngine;

[CreateAssetMenu(fileName = "CoinMagnet Stats", menuName = "Scriptable Objects/Collectables/CoinMagnet Stats")]
public class CoinMagnet_SO : ScriptableObject {

    [SerializeField]
    private string _name = "Coin Magnet";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private float _radius;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public float Duration {
        get { return _duration; }
        set { _duration = value; }
    }

    public float Radius {
        get { return _radius; }
        set { _radius = value; }
    }
}
