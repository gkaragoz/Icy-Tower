using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _offset = 0f;
    [SerializeField]
    private float _deadZoneOffset = 0f;
    [SerializeField]
    CinemachineVirtualCamera vcam = null;
    [SerializeField]
    CinemachineVirtualCamera vcam2 = null;
    [SerializeField]
    CinemachineVirtualCamera vcam3 = null;

    public float scrollSpeed = 0f;

    void LateUpdate() {
        if (_target.position.y + _offset > transform.position.y) {
            vcam.Priority = 20;
            vcam2.Priority = 10;
        } else {
            vcam.Priority = 10;
            vcam2.Priority = 20;
            if (vcam2.transform.position.y > vcam.transform.position.y) {
                vcam2.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed);
            }

            else {
                vcam2.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed * Mathf.Abs(Vector3.Distance(vcam.transform.position, vcam2.transform.position) * 5));
            }
        }
        if (_target.position.y < transform.position.y + _deadZoneOffset) {
            vcam3.Priority = 30;
            vcam3.Follow = _target;
            vcam3.LookAt = _target;
        }
    }
}
