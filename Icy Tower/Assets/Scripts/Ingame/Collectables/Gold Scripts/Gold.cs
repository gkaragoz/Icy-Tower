﻿using System;
using UnityEngine;

public class Gold : MonoBehaviour {

    private Transform _player = null;

    [SerializeField]
    private int _value = 1;
    private float _flySpeedDivisor = 5f;
    private float _minFlySpeed = 0.4f;
    private int _coinScore = 0;
    private bool _hasInteractedWithMagnet = false;
    private Vector3 _initialPosition = Vector3.zero;
    private Quaternion _initialQuaternion = Quaternion.identity;

    private void Awake() {
        _initialPosition = transform.localPosition;
        _initialQuaternion = transform.localRotation;
    }

    public int Value {
        get {
            return _value;
        }
    }
    private void FixedUpdate() {
        if (_hasInteractedWithMagnet) {
            FlyToPlayer();
        }
    }

    public void SetVisibility(bool isActive) {
        gameObject.SetActive(isActive);
        if (isActive) {
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialQuaternion;
            _hasInteractedWithMagnet = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            PlayVFX();
            SetVisibility(false);
            GameManager.instance.AddGoldToPlayer(_coinScore);
            _hasInteractedWithMagnet = false;
        } else if (other.tag == "CoinMagnet") {
            _hasInteractedWithMagnet = true;
            _player = other.gameObject.transform;
        }
    }

    private void PlayVFX() {
        VFX activeVFX = Instantiate(VFXDatabase.instance.GetVFX(VFXTypes.CollectGold));
        activeVFX.transform.position = transform.position;
        activeVFX.Play();
    }

    private void FlyToPlayer() {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, CalculateFlySpeed());
    }

    private float CalculateFlySpeed() {
        if (Vector3.Distance(transform.position, _player.position) / _flySpeedDivisor <= _minFlySpeed)
            return _minFlySpeed;
        return Vector3.Distance(transform.position, _player.position) / _flySpeedDivisor;
    }
}
