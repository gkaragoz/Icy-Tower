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

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "TimeSlower") {
            SlowTime();
            other.gameObject.SetActive(false);
        }
    }

    private void SlowTime() {
        _timeSlower.SetScrollSpeed(CalculateScrollSpeed());
        Camera.main.GetComponent<CameraController>().scrollSpeed = _timeSlower.GetScrollSpeed();
        StartCoroutine(StopSlowingTime());
    }

    private void SpeedUpTime() {
        _timeSlower.SetScrollSpeed(2f);
        Camera.main.GetComponent<CameraController>().scrollSpeed = _timeSlower.GetScrollSpeed();
        _duration = _timeSlower.GetDuration();
    }

    private float CalculateScrollSpeed() {
        return _timeSlower.GetScrollSpeed() / _timeSlower.GetSlowAmount();
    }

    private IEnumerator StopSlowingTime() {
        while (true) {
            _duration--;
            yield return new WaitForSeconds(1f);

            if (_duration <= 0) {
                SpeedUpTime();
                break;
            }
        }
    }

}
