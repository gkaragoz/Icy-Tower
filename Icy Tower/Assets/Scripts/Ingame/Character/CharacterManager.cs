using System;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class CharacterManager : MonoBehaviour {

    [SerializeField]
    private BoxCollider[] _wallColliders = null;
    [SerializeField]
    private Vector3 _initialWardrobeStartPosition = Vector3.zero;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterMotor _characterMotor;

    private Vector3 _initialGameplayStartPosition = Vector3.zero;
    private Vector3 _deadPosition = Vector3.zero;

    private void Awake() {
        _characterMotor = GetComponent<CharacterMotor>();

        _initialGameplayStartPosition = transform.position;

        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void Update() {
        foreach (BoxCollider wallCollider in _wallColliders) {
            wallCollider.transform.position = new Vector3(wallCollider.transform.position.x, transform.position.y - (wallCollider.size.y * 0.5f), wallCollider.transform.position.z);
        }
    }

    private void OnGameStateChanged(GameState previousState, GameState currentState) {
        if (currentState == GameState.MainMenu) {
            _characterMotor.Stop();
            transform.position = _initialWardrobeStartPosition;
        } else if (currentState == GameState.Gameplay) {
            transform.position = _initialGameplayStartPosition;
            _characterMotor.Run();
        } else if (currentState == GameState.GameOver) {
            _characterMotor.Stop();
            _deadPosition = transform.position;
        }
    }

    public void Move(float horizontal) {
        if (_characterMotor.AnimationStateEnum == AnimationState.LeftRun || _characterMotor.AnimationStateEnum == AnimationState.RightRun)
            return;

        _characterMotor.Move(horizontal);
    }

}
