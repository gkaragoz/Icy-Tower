using System.Collections;
using UnityEngine;

public class SpeedUp : MonoBehaviour, IHaveSingleSound {

    [Header("DEBUG")]
    [Utils.ReadOnly]
    [SerializeField]
    private CharacterStats _characterStats = null;
    [Utils.ReadOnly]
    [SerializeField]
    private SpeedUpStats _speedUpStats = null;
    [Utils.ReadOnly]
    [SerializeField]
    private float _duration = 0f;

    private VFX _activeVFX;

    private void Start() {
        _characterStats = GetComponentInParent<CharacterStats>();
        _speedUpStats = GetComponent<SpeedUpStats>();
        _duration = _speedUpStats.GetDuration();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag =="SpeedUp") {
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
        _duration = _speedUpStats.GetDuration();
    }

    private float CalculateSpeedIncrease() {
        return _characterStats.GetMovementSpeed() + _speedUpStats.GetSpeedAmount(); 
    }

    private float CalculateSpeedDecrease() {
        return _characterStats.GetMovementSpeed() - _speedUpStats.GetSpeedAmount();
    }

    private IEnumerator StopSpeedUping() {
        while (true) {
            _duration--;
            yield return new WaitForSeconds(1f);

            if (_duration <= 0) {
                SetCharacterSpeedToNormal();
                StopVFX();
                break;
            }
        }
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
}
