using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerStats))]
public class PlayerController : MonoBehaviour{

    public Joystick _joystick;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterManager _characterManager;
    [Utils.ReadOnly]
    [SerializeField]
    private bool _isMovingLeft = false;
    [Utils.ReadOnly]
    [SerializeField]
    private bool _isMovingRight = false;
    [Utils.ReadOnly]
    [SerializeField]
    private float _horizontal = 0f;

    [SerializeField]
    private PlatformStats _platformStats = null;
    private int _conffettiAmount = 100;
    private int _conffettiCounter = 1;

    private void Awake() {
        _characterManager = GetComponent<CharacterManager>();
    }

    private void Update() {
        if (GameManager.instance.GetGameState() == GameState.Gameplay) {
            int currentFloor = ((int)transform.position.y - (int)PlatformManager.instance.InitialSpawnPosition) / (int)_platformStats.GetDistanceBetweenPlatforms();
            SetScore(currentFloor);
        }
    }

    private void FixedUpdate() {
        if (GameManager.instance.GetGameState() == GameState.Gameplay) {
            _horizontal = Input.GetAxis("Horizontal");
            _horizontal = _joystick.Horizontal;

            if (_isMovingLeft)
                MoveLeft();
            if (_isMovingRight)
                MoveRight();
            if (_horizontal != 0) {
                Move(_horizontal);
            }
            ComboJump();
            Jump();
        }
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

    public void SetMoveLeft(bool moveLeft) {
        _isMovingLeft = moveLeft;
    }
    public void SetMoveRight(bool moveRight) {
        _isMovingRight = moveRight;
    }

    private void PlayVFX() {
        ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXConffetti.ToString(), new Vector3(0, transform.position.y, 0));
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }

    public void SetScore(int currentFloor) {
        if (Account.instance.GetCurrentScore() <= currentFloor) {
            Account.instance.SetCurrentScore(currentFloor);
        }

        if (currentFloor >= _conffettiAmount * _conffettiCounter) {
            _conffettiCounter++;
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_100_Confetti);
        }
    }

}
