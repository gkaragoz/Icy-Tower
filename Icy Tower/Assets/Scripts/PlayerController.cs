using UnityEngine;

[RequireComponent(typeof(CharacterController),typeof(PlayerStats))]
public class PlayerController : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterManager _characterManager;
    [SerializeField]
    [Utils.ReadOnly]
    private PlayerStats _playerStats;

    private void Awake() {
        _characterManager = GetComponent<CharacterManager>();
        _playerStats = GetComponent<PlayerStats>();
    }


    private void FixedUpdate() {
        if (Input.GetAxis("Horizontal") != 0) {
            Move();
        }
        ComboJump();
        Jump();
    }



    public void Jump() {
        _characterManager.Jump();
    }

    public void Move() {
        _characterManager.Move();
    }

    public void ComboJump() {
        _characterManager.ComboJump();
    }

    public void AddGold() {
        _playerStats.AddGold();
    }

}
