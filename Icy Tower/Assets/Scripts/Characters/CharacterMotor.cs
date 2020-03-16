using UnityEngine;

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

    public bool IsFalling {
        get {
            return _rb.velocity.y < 0 ? true : false;
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
        _rb = GetComponentInChildren<Rigidbody>();
        _boxCollider = GetComponentInChildren<BoxCollider>();
    }

    private void Update() {
        if (Physics.Raycast(_boxCollider.transform.position, Vector3.down, out _hit, CollisionRayDistance)) {
            if (_hit.transform.tag == _walkableTag) {
                IsJumping = false;
            }
        } else {
            IsJumping = true;
        }
    }

    public void Jump() {
        //_rb.AddForce(Vector3.up * _characterStats.GetJumpPower(), ForceMode.Impulse);
        _rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }

}
