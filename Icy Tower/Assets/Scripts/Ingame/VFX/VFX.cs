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

    private IEnumerator IStartFollowTarget() {
        while (true) {
            if (_currentTarget == null) {
                break;
            }
            transform.position = _currentTarget.position;
            yield return new WaitForSeconds(0.01f);
        }
        Stop();
    }

    public void SetTarget(Transform target) {
        _currentTarget = target;
    }

    public void Play() {
        this.gameObject.SetActive(true);
        StartCoroutine(IStartFollowTarget());
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
