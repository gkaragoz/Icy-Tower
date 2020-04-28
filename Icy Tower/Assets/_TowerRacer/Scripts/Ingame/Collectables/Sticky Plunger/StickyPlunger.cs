﻿using System;
using System.Collections;
using UnityEngine;

public class StickyPlunger : MonoBehaviour, IHaveSingleSound, IHaveLoopableSound {

    [SerializeField]
    private Transform _player = null;

    [SerializeField]
    private float _forceAmount = 7f;

    [Header("DEBUG")]
    [Utils.ReadOnly]
    [SerializeField]
    private StickyPlungerStats _stickyPlungerStats = null;
    [Utils.ReadOnly]
    [SerializeField]
    private float _climbTime = 0;
    [Utils.ReadOnly]
    [SerializeField]
    private bool _hasUsedStickyPlunger = false;
    [Utils.ReadOnly]
    [SerializeField]
    private bool _isCollideWithRightWall = false;
    [Utils.ReadOnly]
    [SerializeField]
    private bool _isCollideWithLeftWall = false;
    [SerializeField]
    [Utils.ReadOnly]
    private float _wallPositionX = 0f;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterMotor _characterMotor;
    [SerializeField]
    [Utils.ReadOnly]
    private PlayerController _playerController;

    private Rigidbody _rb = null;

    private VFX _activeVFX;
    private Sound _activeSFX;

    private void Start() {
        _rb = GetComponentInParent<Rigidbody>();
        _stickyPlungerStats = GetComponent<StickyPlungerStats>();
        _climbTime = _stickyPlungerStats.GetDuration(); 
        _characterMotor = GetComponentInParent<CharacterMotor>();
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void Update() {
        if (_hasUsedStickyPlunger) {
            if (_isCollideWithRightWall || _isCollideWithLeftWall) {
                StickToWall();
            }
        }
        ChangeWall();
    }

    public void ChangeWall() {
        if (_playerController._joystick == null) {
            return;
        }

        if (_playerController._joystick.Horizontal < 0) {
            if (_isCollideWithRightWall) {
                JumptToOtherWall();
            }
        }
        if (_playerController._joystick.Horizontal > 0) {
            if (_isCollideWithLeftWall) {
                JumptToOtherWall();
            }
        }
    }

    private void JumpToClosestWall() {
        _rb.velocity = Vector3.zero;
        if (gameObject.transform.position.x > 0) {
            _rb.AddForce(new Vector3(1f, 1f) * _forceAmount, ForceMode.Impulse);
        }
        else if (gameObject.transform.position.x <= 0) {
            _rb.AddForce(new Vector3(-1f, 1f) * _forceAmount, ForceMode.Impulse);
        }
        StartCoroutine(StopStickingToWall());
    }

    private void PlayVFX() {
        _activeVFX = ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXPumpWalking.ToString(),transform.position).GetComponent<VFX>();
        _activeVFX.SetTarget(this.transform);
        _activeVFX.Play();
    }

    private void StopVFX() {
        _activeVFX.Stop();
    }

    private void OnTriggerEnter(Collider other) {
        if (_hasUsedStickyPlunger) {
            if (other.tag == "RightWall") {
                _wallPositionX = 3.5f;
                _isCollideWithRightWall = true;
                _isCollideWithLeftWall = false;
                _characterMotor.AnimationStateEnum = AnimationState.RightRun;
            }
            if (other.tag == "LeftWall") {
                _wallPositionX = -3.5f;
                _isCollideWithLeftWall = true;
                _isCollideWithRightWall = false;
                _characterMotor.AnimationStateEnum = AnimationState.LeftRun;

            }
        }
        if (other.tag == "StickyPlunger") {
            _climbTime = _stickyPlungerStats.GetDuration();
            other.gameObject.SetActive(false);
            PlaySFX(SoundFXTypes.InGame_Collect_Slot_Powerup);
            if(_hasUsedStickyPlunger == true) {
                _climbTime = _stickyPlungerStats.GetDuration();
                return;
            }
            _hasUsedStickyPlunger = true;
            LevelManager.instance.IsUsingStickyPlumber = true;
            JumpToClosestWall();
            PlayVFX();
            PlayLoopableSFX();
        }
    }

    private void StickToWall() {
        _player.position = new Vector3(_wallPositionX, _player.position.y, _player.position.z);
        _rb.velocity = Vector3.up * _stickyPlungerStats.GetMoveSpeed();
    }

    private IEnumerator StopStickingToWall() {
        while (true) {
            _climbTime--;

            yield return new WaitForSeconds(1f);

            if (_climbTime <= 0) {
                LeaveWall();
               
                break;
            }
        }
    }

    private void JumptToOtherWall() {
        if (_hasUsedStickyPlunger) {
            _rb.velocity = Vector3.zero;

            PlaySFX(SoundFXTypes.InGame_PowerUp_Pump_Jump);

            if (_isCollideWithRightWall) {
                _isCollideWithRightWall = false;
                _characterMotor.AnimationStateEnum = AnimationState.LeftRun;
                _rb.AddForce(new Vector3(-15, 15, 0), ForceMode.Impulse);
            } else if (_isCollideWithLeftWall) {
                _isCollideWithLeftWall = false;
                _characterMotor.AnimationStateEnum = AnimationState.RightRun;
                _rb.AddForce(new Vector3(15, 15, 0), ForceMode.Impulse);
            }
        }
    }

    private void LeaveWall() {
        _rb.velocity = Vector3.zero;
        _characterMotor.AnimationStateEnum = AnimationState.Jump;
        if (_isCollideWithLeftWall) {
            _rb.AddForce(new Vector3(5, 15, 0), ForceMode.Impulse);
        } else if (_isCollideWithRightWall) {
            _rb.AddForce(new Vector3(-5, 15, 0), ForceMode.Impulse);
        }

        _isCollideWithRightWall = false;
        _isCollideWithLeftWall = false;
        _hasUsedStickyPlunger = false;
        LevelManager.instance.IsUsingStickyPlumber = false;
        StopVFX();
        StopLoopableSFX();

    }

    public void PlayLoopableSFX() {
        _activeSFX = ObjectPooler.instance.SpawnFromPool(SoundFXTypes.InGame_PowerUp_Pump_Step.ToString(), transform.position).GetComponent<Sound>();
        _activeSFX.Play();
    }

    public void StopLoopableSFX() {
        _activeSFX.Stop();
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
}