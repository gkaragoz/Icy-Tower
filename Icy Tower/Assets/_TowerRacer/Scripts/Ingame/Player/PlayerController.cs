using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerStats))]
public class PlayerController : MonoBehaviour {

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

    private bool _isFirstStart = true;

    private void Awake() {
        _characterManager = GetComponent<CharacterManager>();
    }

    private void Update() {
        if (GameManager.instance.GetGameState() == GameState.Gameplay) {
            int currentFloor = ((int)transform.position.y - (int)PlatformManager.instance.InitialSpawnPosition) / (int)_platformStats.GetDistanceBetweenPlatforms();
            SetScore(currentFloor);
            if (_isFirstStart) {
                SetPlayerInitPosition();
               _isFirstStart = false;
            }
        }
    }

    private void SetPlayerInitPosition() {
        if(StartingFloorStats.instance.CalculateStartingPlatformPosition() == 0f) {
            return;
        }
            LeanTween.moveY(gameObject, StartingFloorStats.instance.CalculateStartingPlatformPosition() + 3, 1f);
    }

    private void FixedUpdate() {
        if (GameManager.instance.GetGameState() == GameState.Gameplay) {
            if (UIManager.instance != null) {
                if (UIManager.instance.SelectedControllerType == ControllerType.Joystick) {
                    _horizontal = _joystick.Horizontal;

                    if (_horizontal != 0) {
                        Move(_horizontal);
                    }
                } else if (UIManager.instance.SelectedControllerType == ControllerType.Button) {
                    if (_isMovingLeft) {
                        Move(-1f);
                    } else if (_isMovingRight) {
                        Move(1f);
                    }
                }
            }
        }
    }

    public void Move(float horizontal) {
        _characterManager.Move(horizontal);
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
