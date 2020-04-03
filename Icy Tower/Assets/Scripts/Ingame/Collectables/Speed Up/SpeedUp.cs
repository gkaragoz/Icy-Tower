using System.Collections;
using UnityEngine;

public class SpeedUp : MonoBehaviour{

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

    private void Start() {
        _characterStats = GetComponentInParent<CharacterStats>();
        _speedUpStats = GetComponent<SpeedUpStats>();
        _duration = _speedUpStats.GetDuration();
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.F)) {
            IncreaseCharacterSpeed();
            StartCoroutine(StopSpeedUping());
        }
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
            Debug.Log(_duration);
            yield return new WaitForSeconds(1f);

            if (_duration <= 0) {
                SetCharacterSpeedToNormal();
                break;
            }
        }
    }
}
