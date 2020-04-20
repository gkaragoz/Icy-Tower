using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterManager : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterMotor _characterMotor;

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();

        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState previousState, GameState currentState) {
        if (currentState == GameState.GameOver) {
            _characterMotor.Stop();
        }

        if (currentState == GameState.Gameplay) {
            _characterMotor.Run();
        }
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

}
