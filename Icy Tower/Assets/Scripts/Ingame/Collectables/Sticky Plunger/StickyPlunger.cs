using System;
using System.Collections;
using UnityEngine;

public class StickyPlunger : MonoBehaviour {

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

    private Rigidbody _rb = null;

    private void Start() {
        _rb = GetComponentInParent<Rigidbody>();
        _stickyPlungerStats = GetComponent<StickyPlungerStats>();
        _climbTime = _stickyPlungerStats.GetDuration();
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
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (_isCollideWithRightWall) {
                JumptToOtherWall();
            }
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            if (_isCollideWithLeftWall) {
                JumptToOtherWall();
            }
        }
    }

    private void JumpToClosestWall() {
        _rb.velocity = Vector3.zero;
        if (gameObject.transform.position.x > 0) {
            _rb.AddForce(new Vector3(1f, 1f) * _forceAmount, ForceMode.Impulse);
        } else if (gameObject.transform.position.x <= 0) {
            _rb.AddForce(new Vector3(-1f, 1f) * _forceAmount, ForceMode.Impulse);
        }
        StartCoroutine(StopStickingToWall());
    }

    private void OnTriggerEnter(Collider other) {
        if (_hasUsedStickyPlunger) {
            if (other.tag == "RightWall") {
                _wallPositionX = 3.5f;
                _isCollideWithRightWall = true;
                _isCollideWithLeftWall = false;
            }
            if (other.tag == "LeftWall") {
                _wallPositionX = -3.5f;
                _isCollideWithLeftWall = true;
                _isCollideWithRightWall = false;
            }
        }
        if (other.tag == "StickyPlunger") {
            _climbTime = _stickyPlungerStats.GetDuration();
            _hasUsedStickyPlunger = true;
            JumpToClosestWall();
            other.gameObject.SetActive(false);
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

            if (_isCollideWithRightWall) {
                _isCollideWithRightWall = false;
                _rb.AddForce(new Vector3(-15, 15, 0), ForceMode.Impulse);
            } else if (_isCollideWithLeftWall) {
                _isCollideWithLeftWall = false;
                _rb.AddForce(new Vector3(15, 15, 0), ForceMode.Impulse);
            }
        }
    }

    private void LeaveWall() {
        _rb.velocity = Vector3.zero;

        if (_isCollideWithLeftWall) {
            _rb.AddForce(new Vector3(5, 15, 0), ForceMode.Impulse);
        } else if (_isCollideWithRightWall) {
            _rb.AddForce(new Vector3(-5, 15, 0), ForceMode.Impulse);
        }

        _isCollideWithRightWall = false;
        _isCollideWithLeftWall = false;
        _hasUsedStickyPlunger = false;
    }
}
