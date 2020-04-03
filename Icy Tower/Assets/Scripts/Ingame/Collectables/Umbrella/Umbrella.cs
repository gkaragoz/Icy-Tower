using System;
using System.Collections;
using UnityEngine;

public class Umbrella : MonoBehaviour {

    [Header("DEBUG")]
    [Utils.ReadOnly]
    [SerializeField]
    private Rigidbody _rb = null;
    [Utils.ReadOnly]
    [SerializeField]
    private UmbrellaStats _umbrellaStats = null;
    [Utils.ReadOnly]
    [SerializeField]
    private bool _hasUsedUmbrella = false;
    [SerializeField]
    [Utils.ReadOnly]
    private float _flyTime = 0f;

    private void Start() {
        _rb = GetComponentInParent<Rigidbody>();
        _umbrellaStats = GetComponent<UmbrellaStats>();
        _flyTime = _umbrellaStats.GetDuration();
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.U)){
            _flyTime = _umbrellaStats.GetDuration();
            _hasUsedUmbrella = true;
            StartCoroutine(StopFlying());
        }
        if (_hasUsedUmbrella) {
            StartFly();
        }
    }

    private void StartFly() {
        _rb.velocity = new Vector3(_rb.velocity.x, _umbrellaStats.GetMoveSpeed(), _rb.velocity.z);
    }

    private void StopFly() {
        _hasUsedUmbrella = false;
    }

    private IEnumerator StopFlying() {
        while (true) {
            _flyTime--;
            Debug.Log(_flyTime);
            yield return new WaitForSeconds(1f);

            if (_flyTime <= 0) {
                StopFly();
                break;
            }
        }
    }
}
