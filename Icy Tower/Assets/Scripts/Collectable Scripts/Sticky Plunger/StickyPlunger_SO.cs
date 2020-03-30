using UnityEngine;

[CreateAssetMenu(fileName = "StickyPlunger Stats", menuName = "Scriptable Objects/StickyPlunger Stats")]
public class StickyPlunger_SO : ScriptableObject{

    [SerializeField]
    private string _name = "Sticky Plunger";

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
