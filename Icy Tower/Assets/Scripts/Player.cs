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
    private float _downSpeed = 0f;
    [SerializeField]
    private float _upSpeed = 0f;


    private bool isJumping = false;
    private bool isFalling = true;

    public Animator anim;

    private float _movement = 0f;
    private Vector3 _desiredPosition;
    private Vector3 _scale;
    private bool _isPlayerMoved = false;
    private bool _isRunning = false;
    private bool _hasComboJumped = false;


    void Start() {
        isFalling = true;
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
        if (isFalling) {
            Fall();
        }
        if (isJumping) {
            Jump();
        }
    }

    private void Move() {
        transform.position += new Vector3(_movement * Time.fixedDeltaTime, 0, 0);
    }

    private void PlayComboAnimation() {
        if (_hasComboJumped)
            anim.SetBool("hasComboJumped", _hasComboJumped);
    }

    private void SetDirectionOfCharacter() {
        if (_isPlayerMoved) {
            if (_movement > 0)
                _scale = Vector3.one;
            if (_movement < 0)
                _scale = new Vector3(-1, _gfx.localScale.y, _gfx.localScale.z);
            _gfx.localScale = _scale;
            _isPlayerMoved = false;
        }
    }

    private void PlayRunAnimation() {
        if (_isRunning)
            anim.SetBool("isRunning", _isRunning);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Platform" && isFalling) {
            _desiredPosition = new Vector3(transform.position.x, transform.position.y + _jumpForce, transform.position.z);
            isJumping = true;
            isFalling = false;
            anim.SetBool("hasJumped", isJumping);
        }
    }

    public void Jump() {
        isFalling = false;
        transform.position += Vector3.up * _upSpeed * Time.fixedDeltaTime;
        if (Mathf.Abs(transform.position.y - _desiredPosition.y) <= .1f) {
            isFalling = true;
            isJumping = false;
        }
    }

    private void Fall() {
        isJumping = false;
        isFalling = true;
        transform.position += Vector3.down * _downSpeed * Time.fixedDeltaTime;
    }



}



