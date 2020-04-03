using UnityEngine;

[CreateAssetMenu(fileName = "PlatformSaver Stats", menuName = "Scriptable Objects/Collectables/PlatformSaver Stats")]
public class PlatformSaver_SO : ScriptableObject {

    [SerializeField]
    private string _name = "Platform Saver";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private int _platformCount;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public int PlatformCount {
        get { return _platformCount; }
        set { _platformCount = value; }
    }

}
