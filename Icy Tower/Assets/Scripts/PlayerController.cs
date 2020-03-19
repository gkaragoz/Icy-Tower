using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterManager _characterManager;

    private void Awake() {
        _characterManager = GetComponent<CharacterManager>();
    }


    private void FixedUpdate() {
        if (Input.GetAxis("Horizontal") != 0) {
            Move();
        }
        Jump();
    }



    public void Jump() {
        _characterManager.Jump();
    }

    public void Move() {
        _characterManager.Move();
    }

}
