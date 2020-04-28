using UnityEngine;
using System.Collections;

public class VFX : MonoBehaviour , IPooledObject{

    [SerializeField]
    private bool _isLoopable = false;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private ParticleSystem[] _vfxs = null;
    [SerializeField]
    [Utils.ReadOnly]
    private float _duration = 0f;
    private Transform _currentTarget = null;

    private void Awake() {
        _vfxs = GetComponentsInChildren<ParticleSystem>();
        _duration = _vfxs[0].main.duration;
    }

    private IEnumerator IStartFollowTarget(bool x = true, bool y = true, bool z = true) {
        while (true) {
            if (_currentTarget == null) {
                break;
            }

            float xPos = 0;
            float yPos = 0;
            float zPos = transform.position.z;
            if (x) {
                xPos = _currentTarget.position.x;
            }
            if (y) {
                yPos = _currentTarget.position.y;
            }
            if (z) {
                zPos = _currentTarget.position.z;
            }

            transform.position = new Vector3(xPos, yPos, zPos);
            yield return new WaitForSeconds(0.01f);
        }
        Stop();
    }

    public void SetTarget(Transform target) {
        _currentTarget = target;
    }

    public void Play(bool x = true, bool y = true, bool z = true) {
        this.gameObject.SetActive(true);
        StartCoroutine(IStartFollowTarget(x, y, z));
    }


    public void Stop() {
        this.gameObject.SetActive(false);
        SetTarget(null);
    }

    public void OnObjectReused() {
        if (_isLoopable)
            return;

        this.gameObject.SetActive(true);
        Invoke("Stop",_duration);
    }
}
