using UnityEngine;

[CreateAssetMenu(fileName = "Platform Stats", menuName = "Scriptable Objects/Platform Stats")]
public class PlatformStats_SO : ScriptableObject {

    [SerializeField]
    private string _name = "Platform";

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _maxScale;

    [SerializeField]
    private float _minScale;
    // Movement
    [SerializeField]
    private float _thickness;
    [SerializeField]
    private float _depth;

    [SerializeField]
    private float _movementSpeed = 0;

    [SerializeField]
    private int _jumpPower = 1;

    [SerializeField]
    private float _distanceBetweenPlatforms = 0;


    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public float Thickness {
        get { return _thickness; }
        set { _thickness = value; }
    }

    public float Depth {
        get { return _depth; }
        set { _depth = value; }
    }

    public float DistanceBetweenPlatforms {
        get { return _distanceBetweenPlatforms; }
        set { _distanceBetweenPlatforms = value; }
    }


    public float MovementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public int JumpPower {
        get { return _jumpPower; }
        set { _jumpPower = value; }
    }

    public float MaxScale {
        get { return _maxScale; }
        set { _maxScale = value; }
    }

    public float MinScale {
        get { return _minScale; }
        set { _minScale = value; }
    }
}
