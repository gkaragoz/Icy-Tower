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

    [SerializeField]
    private float _comboJumpPower = 20f;

    [SerializeField]
    private float _maxVelocityX = 8f;

    [SerializeField]
    private float _requiredVelocityForComboJump = 7f;

    [SerializeField]
    private float _footPositionOffset= 0f;

    [SerializeField]
    private float _playerPositionY= 0f;



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

    public float ComboJumpPower {
        get { return _comboJumpPower; }
        set { _comboJumpPower = value; }
    }

    public float MaxVelocityX {
        get { return _maxVelocityX; }
        set { _maxVelocityX = value; }
    }
    public float RequiredVelocityForComboJump {
        get { return _requiredVelocityForComboJump; }
        set { _requiredVelocityForComboJump = value; }
    }

    public float FootPositionOffset {
        get { return _footPositionOffset; }
        set { _footPositionOffset = value; }
    }

    public float CharacterPositionY {
        get { return _playerPositionY; }
        set { _playerPositionY = value; }
    }

}