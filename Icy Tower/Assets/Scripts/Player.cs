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
    private float _fallSpeed = 0f;
    [SerializeField]
    private float _jumpSpeed = 0f;


    private bool isJumping = false;
    private bool isFalling = true;

    public Animator anim;
    public AnimationCurve jumpCurve;
    public AnimationCurve fallCurve;

    private float _timer = 0f;
    private float _movement = 0f;
    private float _maxDistance = 0f;
    private float _distanceToFall = 0f;
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
            _timer = 0f;
            _desiredPosition = new Vector3(transform.position.x, transform.position.y + _jumpForce, transform.position.z);
            _maxDistance = Mathf.Abs(transform.position.y - _desiredPosition.y);
            isJumping = true;
            isFalling = false;
            anim.SetBool("hasJumped", isJumping);
        }
    }

    public void Jump() {
        _distanceToFall = Mathf.Abs(transform.position.y - _desiredPosition.y);
        isFalling = false;
        transform.position += Vector3.up * Time.fixedDeltaTime * _jumpSpeed * jumpCurve.Evaluate((_distanceToFall/_maxDistance));

        if (Mathf.Abs(transform.position.y - _desiredPosition.y) <= .1f) {
            Debug.Log("Start falling");
            _timer = 0f;
            isFalling = true;
            isJumping = false;
        }
    }

    private void Fall() {
        isJumping = false;
        isFalling = true;
        _timer += Time.deltaTime;
        transform.position += Vector3.down * Time.fixedDeltaTime * _fallSpeed * fallCurve.Evaluate(_timer);
    }

}



