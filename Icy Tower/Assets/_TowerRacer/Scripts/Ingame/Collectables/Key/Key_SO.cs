using UnityEngine;

[CreateAssetMenu(fileName = "Key Stats", menuName = "Scriptable Objects/Collectables/Key")]
public class Key_SO : ScriptableObject {

    [SerializeField]
    private string _name = "Key";

    [SerializeField]
    private GameObject _prefab = null;

    [SerializeField]
    [Utils.ReadOnly]
    private int _amount = 1;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public int Amount {
        get { return _amount; }
        set { _amount = value; }
    }
}
