﻿using System;
using System.Collections;
using UnityEngine;

public class Umbrella : MonoBehaviour, IHaveSingleSound {

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

    private VFX _activeVFX;

    private void Start() {
        _rb = GetComponentInParent<Rigidbody>();
        _umbrellaStats = GetComponent<UmbrellaStats>();
        _flyTime = _umbrellaStats.GetDuration();
    }

    private void FixedUpdate() {
        if (_hasUsedUmbrella) {
            StartFly();
        }
    }
    private void PlayVFX() {
        _activeVFX = ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXUmbrella.ToString(), transform.position).GetComponent<VFX>();
        _activeVFX.SetTarget(this.transform);
        _activeVFX.Play(false, true, false);
    }

    private void StopVFX() {
        _activeVFX.Stop();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Umbrella") {
            _flyTime = _umbrellaStats.GetDuration();
            _hasUsedUmbrella = true;
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_Slot_Powerup);
            StartCoroutine(StopFlying());
            other.gameObject.SetActive(false);
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
                StopVFX();
                break;
            }
        }
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
}
