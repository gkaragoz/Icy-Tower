using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private float _jumpPower = 100f;
    [SerializeField]
    private float _collisionRayDistance = 1f;
    [SerializeField]
    private string _walkableTag = "Platform";

    [Header("Debug")]
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private BoxCollider _boxCollider;
    [SerializeField]
    private RaycastHit _hit;
    [SerializeField]
    private bool _isJumping = false;

    public bool IsFalling { 
        get {
            return _rb.velocity.y < 0 ? true : false;
        } 
    }

    private void Awake() {
        _rb = GetComponentInChildren<Rigidbody>();
        _boxCollider = GetComponentInChildren<BoxCollider>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping) {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }

        if (Physics.Raycast(_boxCollider.transform.position, Vector3.down, out _hit, _collisionRayDistance)) {
            if (_hit.transform.tag == _walkableTag) {
                _isJumping = false;
            }
        } else {
            _isJumping = true;
        }

        HandleColliders();
    }

    private void HandleColliders() {
        if (IsFalling) {
            SetColliders(true);
        } else if (_isJumping) {
            SetColliders(false);
        }
    }

    private void SetColliders(bool status) {
        _boxCollider.enabled = status;
    }

    private void OnDrawGizmos() {
        if (_boxCollider == null) {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(_boxCollider.transform.position, Vector3.down * _collisionRayDistance);
    }

}
