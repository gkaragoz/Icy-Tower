using UnityEngine;

[CreateAssetMenu(fileName = "Umbrella Stats", menuName = "Scriptable Objects/Collectables/Umbrella Stats")]
public class Umbrella_SO : ScriptableObject {

    [SerializeField]
    private string _name = "Umbrella";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private float _moveSpeed;

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

    public float MoveSpeed {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
}


