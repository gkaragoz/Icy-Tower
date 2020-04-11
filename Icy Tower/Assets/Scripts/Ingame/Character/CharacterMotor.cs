﻿using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterMotor : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private float _collisionRayDistance = 1f;
    [SerializeField]
    private string _jumpableTag = "Platform";

    public Action<AnimationState> OnAnimationStateChanged;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Rigidbody _rb;
    [SerializeField]
    [Utils.ReadOnly]
    private BoxCollider _boxCollider;
    [SerializeField]
    [Utils.ReadOnly]
    private RaycastHit _hit;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isJumping = false;
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isJumpCalled = false;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterStats _characterStats;
    [SerializeField]
    [Utils.ReadOnly]
    private AnimationState _animationState = AnimationState.Idle;
    [SerializeField]
    [Utils.ReadOnly]
    private VFX _activeVFX = null;

    public AnimationState AnimationStateEnum {
        get {
            return _animationState;
        }
        set {
            _animationState = value;
            OnAnimationStateChanged?.Invoke(_animationState);
        }
    }

    public bool IsFalling {
        get {
            return _rb.velocity.y < 0 ? true : false;
        }
    }

    public bool IsJumping {
        get {
            return _isJumping;
        }
        private set {
            _isJumping = value;
        }
    }

    public bool IsMoving {
        get {
            return Input.GetAxis("Horizontal") != 0 ? true : false;
        }
    }

    public float CollisionRayDistance {
        get {
            return _collisionRayDistance;
        }
    }

    public BoxCollider BoxCollider {
        get {
            return _boxCollider;
        }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody>();

        _boxCollider = GetComponentInChildren<BoxCollider>();
        _characterStats = GetComponent<CharacterStats>();
    }

    private void FixedUpdate() {
        ApplyLocalGravity();

        SendRay();
        SetCharacterPositionY();
        GameManager.instance.SetScore();
    }

    private void ApplyLocalGravity() {
        _rb.AddForce(Vector3.up * Physics.gravity.y * _characterStats.GetLocalGravity(), ForceMode.Acceleration);
    }

    private void SendRay() {
        Vector3 _leftFoot = new Vector3(_boxCollider.transform.position.x + _characterStats.GetFootPositionOffset(), _boxCollider.transform.position.y, _boxCollider.transform.position.z);
        Vector3 _rightFoot = new Vector3(_boxCollider.transform.position.x - _characterStats.GetFootPositionOffset(), _boxCollider.transform.position.y, _boxCollider.transform.position.z);
        if (Physics.Raycast(_leftFoot, Vector3.down, out _hit, CollisionRayDistance) || Physics.Raycast(_rightFoot, Vector3.down, out _hit, CollisionRayDistance)) {
            if (_hit.transform.tag == _jumpableTag && IsFalling == true) {
                IsJumping = false;
                StopLoopVFX();
            }
        } else {
            IsJumping = true;
            _isJumpCalled = false;
        }
    }

    public void Jump() {
        if (!_isJumpCalled) {
            _isJumpCalled = true;
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(Vector3.up * (_characterStats.GetJumpPower() + (Mathf.Abs(_rb.velocity.x) / 3f)), ForceMode.Impulse);
            AnimationStateEnum = AnimationState.Jump;
            PlayVFX();
        }
    }

    private void PlayVFX() {
        ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXJump.ToString(), transform.position);
    }

    private void PlayLoopVFX() {
        _activeVFX = ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXComboJump.ToString(), transform.position).GetComponent<VFX>();
        _activeVFX.SetTarget(this.transform);
        _activeVFX.Play();
    }

    private void StopLoopVFX() {
        if (_activeVFX == null)
            return;
        _activeVFX.Stop();
    }

    public void ComboJump() {
        if (!_isJumpCalled) {
            if (Mathf.Abs(_rb.velocity.x) >= _characterStats.GetRequiredVelocityForComboJump()) {
                _isJumpCalled = true;
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                _rb.AddForce(Vector3.up * _characterStats.GetComboJumpPower(), ForceMode.Impulse);
                AnimationStateEnum = AnimationState.ComboJump;
                PlayLoopVFX();
            }
        }
    }

    public void Move(float horizontal) {
        _rb.AddForce(new Vector3(horizontal * _characterStats.GetMovementSpeed(), 0));
    }

    public void MoveLeft() {
        if (_rb.velocity.x > _characterStats.GetMaxVelocityX())
            _rb.velocity = new Vector3(_characterStats.GetMaxVelocityX(), _rb.velocity.y, _rb.velocity.z);
        if (_rb.velocity.x < -_characterStats.GetMaxVelocityX())
            _rb.velocity = new Vector3(-_characterStats.GetMaxVelocityX(), _rb.velocity.y, _rb.velocity.z);

        _rb.AddForce(new Vector3(-1 * _characterStats.GetMovementSpeed(), 0));
    }

    public void MoveRight() {
        if (_rb.velocity.x > _characterStats.GetMaxVelocityX())
            _rb.velocity = new Vector3(_characterStats.GetMaxVelocityX(), _rb.velocity.y, _rb.velocity.z);
        if (_rb.velocity.x < -_characterStats.GetMaxVelocityX())
            _rb.velocity = new Vector3(-_characterStats.GetMaxVelocityX(), _rb.velocity.y, _rb.velocity.z);

        _rb.AddForce(new Vector3(1 * _characterStats.GetMovementSpeed(), 0));
    }

    private void SetCharacterPositionY() {
        _characterStats.SetCharacterPositionY(gameObject.transform.position.y);
    }

}
