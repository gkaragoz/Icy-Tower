using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterMotor : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private float _collisionRayDistance = 1f;
    [SerializeField]
    private string _walkableTag = "Platform";

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Rigidbody _rb;
    [SerializeField]
    [Utils.ReadOnly]
    private BoxCollider _boxCollider;
    [SerializeField]
    [Utils.ReadOnly]
    private RaycastHit _hit;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isJumping = false;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isJumpCalled = false;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterStats _characterStats;


    public bool IsFalling {
        get {
            return _rb.velocity.y < 0 ? true : false;
        }
    }

    public bool IsMoving {
        get {
            return Input.GetAxis("Horizontal") != 0 ? true : false;

        }
    }

    public bool IsJumping {
        get {
            return _isJumping;
        }
        private set {
            _isJumping = value;
        }
    }

    public float CollisionRayDistance {
        get {
            return _collisionRayDistance;
        }
    }

    public BoxCollider BoxCollider {
        get {
            return _boxCollider;
        }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponentInChildren<BoxCollider>();
        _characterStats = GetComponent<CharacterStats>();
    }

    private void Update() {
        SendRay();
    }

    private void SendRay() {
        Vector3 _leftFoot = new Vector3(_boxCollider.transform.position.x + _characterStats.GetFootPositionOffset(), _boxCollider.transform.position.y, _boxCollider.transform.position.z);
        Vector3 _rightFoot = new Vector3(_boxCollider.transform.position.x - _characterStats.GetFootPositionOffset(), _boxCollider.transform.position.y, _boxCollider.transform.position.z);
        if (Physics.Raycast(_leftFoot, Vector3.down, out _hit, CollisionRayDistance) || Physics.Raycast(_rightFoot, Vector3.down, out _hit, CollisionRayDistance)) {
            if (_hit.transform.tag == _walkableTag && IsFalling == true) {
                IsJumping = false;
            }
        } else {
            IsJumping = true;
            _isJumpCalled = false;
        }
    }

    public void Jump() {
        if (!_isJumpCalled) {
            _isJumpCalled = true;
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(Vector3.up * _characterStats.GetJumpPower(), ForceMode.Impulse);
        }
    }

    public void ComboJump() {
        if (!_isJumpCalled) {
            if (Mathf.Abs(_rb.velocity.x) >= _characterStats.GetRequiredVelocityForComboJump()) {
                _isJumpCalled = true;
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                _rb.AddForce(Vector3.up * _characterStats.GetComboJumpPower(), ForceMode.Impulse);
            }
        }
    }

    public void Move() {
        float _horizontalMove = Input.GetAxis("Horizontal");

        if (_rb.velocity.x > _characterStats.GetMaxVelocityX())
            _rb.velocity = new Vector3(_characterStats.GetMaxVelocityX(), _rb.velocity.y, _rb.velocity.z);
        if (_rb.velocity.x < -_characterStats.GetMaxVelocityX())
            _rb.velocity = new Vector3(-_characterStats.GetMaxVelocityX(), _rb.velocity.y, _rb.velocity.z);

        _rb.AddForce(new Vector3(_horizontalMove * _characterStats.GetMovementSpeed(), 0));

    }

}
