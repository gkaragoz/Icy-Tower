using System.Collections;
using UnityEngine;

public class TimeSlower : MonoBehaviour {

    [Header("DEBUG")]
    [Utils.ReadOnly]
    [SerializeField]
    private TimeSlowerStats _timeSlower = null;
    [SerializeField]
    [Utils.ReadOnly]
    private float _duration = 0f;

    private void Start() {
        _timeSlower = GetComponent<TimeSlowerStats>();
        _duration = _timeSlower.GetDuration();
    }

    private void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.S)) {
            SlowTime();
        }
    }

    private void SlowTime() {
        _timeSlower.SetScrollSpeed(CalculateScrollSpeed());
        StartCoroutine(StopSlowingTime());
    }

    private void SpeedUpTime() {
        _timeSlower.SetScrollSpeed(2f);
        _duration = _timeSlower.GetDuration();
    }

    private float CalculateScrollSpeed() {
        return _timeSlower.GetScrollSpeed() / _timeSlower.GetSlowAmount();
    }

    private IEnumerator StopSlowingTime() {
        while (true) {
            _duration--;
            Debug.Log(_duration);
            yield return new WaitForSeconds(1f);

            if (_duration <= 0) {
                SpeedUpTime();
                break;
            }
        }
    }

}
