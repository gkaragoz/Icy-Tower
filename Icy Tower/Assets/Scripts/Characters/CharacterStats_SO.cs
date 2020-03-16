using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "Scriptable Objects/Character Stats")]
public class CharacterStats_SO : ScriptableObject {

    [SerializeField]
    private string _name = "Character";

    [SerializeField]
    private GameObject _prefab;

    // Movement
    [SerializeField]
    private float _movementSpeed = 5f;

    // Jump
    [SerializeField]
    private float _jumpPower = 10f;

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public GameObject Prefab {
        get { return _prefab; }
        set { _prefab = value; }
    }

    public float MovementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public float JumpPower {
        get { return _jumpPower; }
        set { _jumpPower = value; }
    }

}