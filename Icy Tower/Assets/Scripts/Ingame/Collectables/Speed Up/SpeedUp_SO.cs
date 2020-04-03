using UnityEngine;

[CreateAssetMenu(fileName ="SpeedUp Stats" , menuName = "Scriptable Objects/SpeedUp Stats")]
public class SpeedUp_SO : ScriptableObject{


    [SerializeField]
    private string _name = "Speed Up";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private float _speedAmount;

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

    public float SpeedAmount {
        get { return _speedAmount; }
        set { _speedAmount = value; }
    }

}
