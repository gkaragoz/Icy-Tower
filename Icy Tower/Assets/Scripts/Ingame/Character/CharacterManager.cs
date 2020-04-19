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

    public void Move(float horizontal) {
        if (_characterMotor.AnimationStateEnum == AnimationState.LeftRun || _characterMotor.AnimationStateEnum == AnimationState.RightRun)
            return;

        _characterMotor.Move(horizontal);
    }

    public void MoveLeft() {
        _characterMotor.MoveLeft();
    }
    public void MoveRight() {
        _characterMotor.MoveRight();
    }

    private void OnDrawGizmos() {
        if (_characterMotor == null || _characterMotor.BoxCollider == null) {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(_characterMotor.BoxCollider.transform.position, Vector3.down * _characterMotor.CollisionRayDistance);
    }

}
