using UnityEngine;

[RequireComponent(typeof(CharacterController),typeof(PlayerStats))]
public class PlayerController : MonoBehaviour {

    public PlayerStats PlayerStats { get { return _playerStats; } }

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterManager _characterManager;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterStats _characterStats;
    [SerializeField]
    [Utils.ReadOnly]
    private PlayerStats _playerStats;

    private void Awake() {
        _characterManager = GetComponent<CharacterManager>();
        _playerStats = GetComponent<PlayerStats>();
        _characterStats = GetComponent<CharacterStats>();
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

    public void SetScore() {
        int currentScore = _playerStats.GetCurrentScore();
        if(_characterStats.GetCharacterPositionY() > currentScore) {
            PlayerStats.SetCurrentScore((int)_characterStats.GetCharacterPositionY());
        }
    }
}
