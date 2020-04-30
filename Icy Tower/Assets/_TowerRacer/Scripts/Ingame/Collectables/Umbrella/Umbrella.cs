using System;
using System.Collections;
using UnityEngine;

public class Umbrella : MonoBehaviour, IHaveSingleSound {

    [Header("DEBUG")]
    [Utils.ReadOnly]
    [SerializeField]
    private Rigidbody _rb = null;
    [SerializeField]
    private MarketItem _marketItem= null;
    [Utils.ReadOnly]
    [SerializeField]
    private bool _hasUsedUmbrella = false;
    [SerializeField]
    private float _flyTime = 0f;
    private float _tempFlyTime = 0f;
    private float _flySpeed = 15f;


    private VFX _activeVFX;

    private void Start() {
        _marketItem.OnMarketItemUpdated += CalculateNewStats;
        CalculateNewStats();
        _tempFlyTime = _flyTime;
        _rb = GetComponentInParent<Rigidbody>();
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
            _tempFlyTime = _flyTime;
            _hasUsedUmbrella = true;
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_Slot_Powerup);
            StartCoroutine(StopFlying());
            other.gameObject.SetActive(false);
            LevelManager.instance.IsUsingUmbrella = true;
        }
    }

    private void StartFly() {
        _rb.velocity = new Vector3(_rb.velocity.x, _flySpeed, _rb.velocity.z);
    }

    private void StopFly() {
        _hasUsedUmbrella = false;
        LevelManager.instance.IsUsingUmbrella = false;
        _tempFlyTime = _flyTime;
    }

    private IEnumerator StopFlying() {
        while (true) {
            _tempFlyTime--;
            yield return new WaitForSeconds(1f);

            if (_tempFlyTime <= 0) {
                StopFly();
                StopVFX();
                break;
            }
        }
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
    
    private void CalculateNewStats() {
        _flyTime += _marketItem.GetCurrentLevel();
        _tempFlyTime = _flyTime;
    }
}
