using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerStats))]
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
    [Utils.ReadOnly]
    [SerializeField]
    private bool _isMovingLeft = false;
    [Utils.ReadOnly]
    [SerializeField]
    private bool _isMovingRight = false;

    private void Awake() {
        _characterManager = GetComponent<CharacterManager>();
        _playerStats = GetComponent<PlayerStats>();
        _characterStats = GetComponent<CharacterStats>();
    }


    private void FixedUpdate() {
        float horizontal = Input.GetAxis("Horizontal");

        if (_isMovingLeft)
            MoveLeft();
        if (_isMovingRight)
            MoveRight();
        if (horizontal != 0) {
            Move(horizontal);
        }
        ComboJump();
        Jump();
    }

    public void Move(float horizontal) {
        _characterManager.Move(horizontal);
    }

    public void Jump() {
        _characterManager.Jump();
    }

    public void MoveLeft() {
        _characterManager.MoveLeft();
    }

    public void MoveRight() {
        _characterManager.MoveRight();
    }

    public void ComboJump() {
        _characterManager.ComboJump();
    }

    public void AddGold(int value) {
        _playerStats.AddGold(value);
    }

    public void AddKey(int value) {
        _playerStats.AddKey(value);
    }

    public void SetScore() {
        int currentScore = _playerStats.GetCurrentScore();
        if (_characterStats.GetCharacterPositionY() > currentScore) {
            PlayerStats.SetCurrentScore((int)_characterStats.GetCharacterPositionY());
        }
    }

    public void SetMoveLeft(bool moveLeft) {
        _isMovingLeft = moveLeft;
    }
    public void SetMoveRight(bool moveRight) {
        _isMovingRight = moveRight;
    }
}
