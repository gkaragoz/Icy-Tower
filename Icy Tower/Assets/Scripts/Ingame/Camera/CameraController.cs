using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _offset = 0f;
    [SerializeField]
    private float _smothSpeed = 0f;
    [SerializeField]
    CinemachineVirtualCamera vcam = null;
    [SerializeField]
    CinemachineVirtualCamera vcam2 = null;

    public float scrollSpeed = 0f;

    void LateUpdate() {
        if (_target.position.y + _offset > transform.position.y) {
            vcam.Priority = 20;
            vcam2.Priority = 10;
            //vcam2.transform.position = new Vector3(vcam.transform.position.x, vcam.transform.position.y -1f, vcam.transform.position.z);
            //Vector3 newPos = new Vector3(transform.position.x, _target.position.y + _offset, transform.position.z);
            //transform.position = Vector3.Lerp(transform.position, newPos, _smothSpeed);
        } else {
            vcam.Priority = 10;
            vcam2.Priority = 20;
            if (vcam2.transform.position.y > vcam.transform.position.y)
                vcam2.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed);
            else {
                vcam2.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed * Mathf.Abs(Vector3.Distance(vcam.transform.position, vcam2.transform.position) * 5));
            }
        }
    }
}
//cinemachine