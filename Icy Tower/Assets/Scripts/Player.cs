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
    private float _comboJumpForce = 0f;
    [SerializeField]
    private float smoothSpeed = 0f;
    [SerializeField]
    private float _downSpeed = 0f;


    private bool isJumping;
    private bool isFalling;

    public Animator anim;

    private Rigidbody2D _rb;

    private float _movement = 0f;
    private Vector3 _desiredPosition;
    private Vector3 _scale;
    private bool _isPlayerMoved = false;
    private bool _isRunning = false;
    private bool _hasComboJumped = false;


    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate() {
        _movement = _joystick.Horizontal * _movementSpeed;

        Vector2 velocity = _rb.velocity;
        velocity.x = _movement;
        if (velocity.x != 0) {
            _isPlayerMoved = true;
            _isRunning = true;
        } else {
            _isRunning = false;
        }
        SetDirectionOfCharacter(velocity);
        PlayRunAnimation();
        PlayComboAnimation();
        _rb.velocity = velocity;

        if (!isJumping) {
            Fall();
        }
    }

    private void PlayComboAnimation() {
        if (_hasComboJumped)
            anim.SetBool("hasComboJumped", _hasComboJumped);
    }

    private void SetDirectionOfCharacter(Vector2 velocity) {
        if (_isPlayerMoved) {
            SetScaleBasedOnVelocity(velocity);
        }
    }

    private void SetScaleBasedOnVelocity(Vector2 velocity) {
        if (velocity.x > 0)
            _scale = Vector3.one;
        if (velocity.x < 0)
            _scale = new Vector3(-1, _gfx.localScale.y, _gfx.localScale.z);
        _gfx.localScale = _scale;
        _isPlayerMoved = false;
    }

    private void PlayRunAnimation() {
        if (_isRunning)
            anim.SetBool("isRunning", _isRunning);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Platform" && isFalling) {
            isJumping = true;
            isFalling = false;
            RealJump();
            anim.SetBool("hasJumped", isJumping);
        }
    }


    public void RealJump() {
        _desiredPosition = new Vector3(transform.position.x, transform.position.y + _jumpForce, transform.position.z);
        StartCoroutine(GoToPosition());
    }

    IEnumerator GoToPosition() {
        while (isJumping) {
            transform.position = Vector3.Lerp(transform.position, _desiredPosition, 0.0250f);
            if (Vector3.Distance(transform.position, _desiredPosition) <= 0.15f) {
                isJumping = false;
            }
            yield return null;
        }
    }

    private void Fall() {
        isFalling = true;
        transform.position += Vector3.down * _downSpeed * Time.fixedDeltaTime;
        Debug.Log("Falling");
    }



}



