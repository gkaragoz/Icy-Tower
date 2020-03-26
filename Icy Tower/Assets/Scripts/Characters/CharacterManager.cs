using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterManager : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterMotor _characterMotor;

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();
    }

    private void Update() {
        HandleColliders();
    }

    public void Jump() {
        if (_characterMotor.IsJumping) {
            return;
        }

        _characterMotor.Jump();
    }

    public void ComboJump() {
        if (_characterMotor.IsJumping) {
            return;
        }
        _characterMotor.ComboJump();
    }

    public void MoveLeft() {
            _characterMotor.MoveLeft();
    }
    public void MoveRight() {
            _characterMotor.MoveRight();
    }

    private void HandleColliders() {
        if (_characterMotor.IsFalling) {
            SetColliders(true);
        } else if (_characterMotor.IsJumping) {
            SetColliders(false);
        }
    }

    private void SetColliders(bool status) {
        _characterMotor.BoxCollider.enabled = status;
    }

    private void OnDrawGizmos() {
        if (_characterMotor == null || _characterMotor.BoxCollider == null) {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(_characterMotor.BoxCollider.transform.position, Vector3.down * _characterMotor.CollisionRayDistance);
    }

}
