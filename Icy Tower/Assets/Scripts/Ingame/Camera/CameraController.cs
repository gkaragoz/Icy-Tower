using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _offset = 0f;
    [SerializeField]
    private float _smothSpeed = 0f;

    public float scrollSpeed = 0f;

    void LateUpdate() {
        if (_target.position.y + _offset > transform.position.y) {
            Vector3 newPos = new Vector3(transform.position.x, _target.position.y + _offset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, _smothSpeed);
        } else {
            transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed);
        }
    }
}
//cinemachine