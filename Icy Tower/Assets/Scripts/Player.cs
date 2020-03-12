using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _movementSpeed = 0f;
    [SerializeField]
    private Joystick _joystick = null;
    [SerializeField]
    private Transform _gfx = null;
    [SerializeField]
    private float _jumpForce = 0f;
    [SerializeField]
    private float _airSpeed = 0f;
    [SerializeField]
    private float _comboMultiplier = 0f;


    private bool _isJumping = false;
    private bool _isFalling = true;

    public Animator anim;
    public AnimationCurve jumpCurve;
    public AnimationCurve fallCurve;
    public AnimationCurve moveCurve;


    private float _timer = 0f;
    private float _leftMoveTimer = 0f;
    private float _rightMoveTimer = 0f;
    private float _movement = 0f;
    private float _maxDistance = 0f;
    private float _distanceToFall = 0f;
    private float _rightSubstitutionAmount = 0f;
    private float _leftSubstitutionAmount = 0f;
    private Vector3 _desiredPosition;
    private Vector3 _scale;
    private bool _isPlayerMoved = false;
    private bool _isRunning = false;
    private bool _hasComboJumped = false;


    void Start() {
        _isFalling = true;
        _hasComboJumped = false;
    }


    void FixedUpdate() {


        _movement = _joystick.Horizontal * _movementSpeed;
        Move();

        if (_movement != 0) {
            _isPlayerMoved = true;
            _isRunning = true;
        } else {
            _isRunning = false;
        }

        SetDirectionOfCharacter();
        PlayRunAnimation();
        PlayComboAnimation();

        if (_isFalling) {
            Fall();
        }
        if (_isJumping) {
            Jump();
        }
    }

    private void Move() {
        if (_movement > 0) {
            _rightSubstitutionAmount = _movement * Time.fixedDeltaTime * (moveCurve.Evaluate(_rightMoveTimer * 0.8f));
            transform.position += new Vector3(_rightSubstitutionAmount, 0, 0);

        }
        if (_movement < 0) {
            _leftSubstitutionAmount = _movement * Time.fixedDeltaTime * (moveCurve.Evaluate(_leftMoveTimer * 0.8f));
            transform.position += new Vector3(_leftSubstitutionAmount, 0, 0);
        }
    }

    private void PlayComboAnimation() {
        if (_hasComboJumped)
            anim.SetBool("hasComboJumped", _hasComboJumped);
    }

    private void SetDirectionOfCharacter() {
        if (_isPlayerMoved) {
            if (_movement > 0) {
                _scale = Vector3.one;
                _leftMoveTimer = 0f;
                _rightMoveTimer += Time.fixedDeltaTime;

            }
            if (_movement < 0) {
                _scale = new Vector3(-1, _gfx.localScale.y, _gfx.localScale.z);
                _rightMoveTimer = 0f;
                _leftMoveTimer += Time.fixedDeltaTime;
            }
            _gfx.localScale = _scale;
            _isPlayerMoved = false;
        }
    }

    private void PlayRunAnimation() {
        if (_isRunning)
            anim.SetBool("isRunning", _isRunning);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Platform" && _isFalling) {
            if (_rightSubstitutionAmount > 0.15f || _leftSubstitutionAmount < -0.15f)
                _hasComboJumped = true;

            PlaySounds();
            _timer = 0f;
            GetDesiredJumpDistance();
            _maxDistance = Mathf.Abs(transform.position.y - _desiredPosition.y);
            _isJumping = true;
            _isFalling = false;
            anim.SetBool("hasJumped", _isJumping);
        }
    }

    private void PlaySounds() {
        if (_hasComboJumped)
            SoundManager.PlayComboJumpSound();
        else {
            SoundManager.PlayJumpSound();
        }
    }
    private void GetDesiredJumpDistance() {
        if (_hasComboJumped) {
            _desiredPosition = new Vector3(transform.position.x, transform.position.y + (_comboMultiplier * _jumpForce), transform.position.z);
        } else {
            _desiredPosition = new Vector3(transform.position.x, transform.position.y + _jumpForce, transform.position.z);
        }
    }

    public void Jump() {
        _isFalling = false;


        _distanceToFall = Mathf.Abs(transform.position.y - _desiredPosition.y);

        if (_hasComboJumped) {
            transform.position += Vector3.up * Time.fixedDeltaTime * (_airSpeed + _comboMultiplier ) * jumpCurve.Evaluate((_distanceToFall / _maxDistance));
        } else {
            transform.position += Vector3.up * Time.fixedDeltaTime * (_airSpeed) * jumpCurve.Evaluate((_distanceToFall / _maxDistance));
        }

        if (Mathf.Abs(transform.position.y - _desiredPosition.y) <= .2f) {

            _timer = 0f;
            _isFalling = true;
            _isJumping = false;
        }
    }

    private void Fall() {
        _isJumping = false;
        _isFalling = true;
        _hasComboJumped = false;
        _timer += Time.deltaTime;
        transform.position += Vector3.down * Time.fixedDeltaTime * _airSpeed * fallCurve.Evaluate(_timer);
    }

}



