using System.Collections;
using UnityEngine;

public class SpeedUp : MonoBehaviour, IHaveSingleSound {

    [Header("DEBUG")]
    [Utils.ReadOnly]
    [SerializeField]
    private CharacterStats _characterStats = null;
    [SerializeField]
    private MarketItem _marketItem = null;
    [SerializeField]
    private float _duration = 0f;
    private float _tempDuration = 0f;
    private float _speedAmount = 3f;


    private VFX _activeVFX;

    private void Start() {
        _marketItem.OnMarketItemUpdated += CalculateNewStats;
        CalculateNewStats();
        _tempDuration = _duration;
        _characterStats = GetComponentInParent<CharacterStats>();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "SpeedUp") {
            IncreaseCharacterSpeed();
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_Slot_Powerup);
            StartCoroutine(StopSpeedUping());
            other.gameObject.SetActive(false);
        }
    }

    private void PlayVFX() {
        _activeVFX = ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXGhost.ToString(), transform.position).GetComponent<VFX>();
        _activeVFX.SetTarget(this.transform);
        _activeVFX.Play();
    }

    private void StopVFX() {
        _activeVFX.Stop();
    }

    private void IncreaseCharacterSpeed() {
        _characterStats.SetMovementSpeed(CalculateSpeedIncrease());
    }

    private void SetCharacterSpeedToNormal() {
        _characterStats.SetMovementSpeed(CalculateSpeedDecrease());
        _tempDuration = _duration;
    }

    private float CalculateSpeedIncrease() {
        return _characterStats.GetMovementSpeed() + _speedAmount;
    }

    private float CalculateSpeedDecrease() {
        return _characterStats.GetMovementSpeed() - _speedAmount;
    }

    private IEnumerator StopSpeedUping() {
        while (true) {
            _tempDuration--;
            yield return new WaitForSeconds(1f);

            if (_tempDuration <= 0) {
                SetCharacterSpeedToNormal();
                StopVFX();
                break;
            }
        }
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }

    private void CalculateNewStats() {
        _duration = _duration + _marketItem.GetCurrentLevel();
        _tempDuration = _duration;
    }
}
