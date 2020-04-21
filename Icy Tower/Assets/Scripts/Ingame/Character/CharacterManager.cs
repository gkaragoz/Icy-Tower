using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterManager : MonoBehaviour {

    [SerializeField]
    private BoxCollider[] _wallColliders = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterMotor _characterMotor;

    private Vector3 _initialStartPosition = Vector3.zero;
    private Vector3 _deadPosition = Vector3.zero;

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();

        _initialStartPosition = transform.position;

        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void Update() {
        foreach (BoxCollider wallCollider in _wallColliders) {
            wallCollider.transform.position = new Vector3(wallCollider.transform.position.x, transform.position.y - (wallCollider.size.y * 0.5f), wallCollider.transform.position.z);
        }
    }

    private void OnGameStateChanged(GameState previousState, GameState currentState) {
        if (currentState == GameState.GameOver) {
            _characterMotor.Stop();
            _deadPosition = transform.position;
        }

        if (currentState == GameState.Gameplay) {
            transform.position = _initialStartPosition;
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
