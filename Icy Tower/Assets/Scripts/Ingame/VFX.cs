using UnityEngine;

public class VFX : MonoBehaviour, IPooledObject {

    [SerializeField]
    private ParticleSystem _vfx;
    [SerializeField]
    private VFXTypes _vfxType;

    [SerializeField]
    [Utils.ReadOnly]
    private float _duration = 0f;

    private void Awake() {
        _duration = _vfx.main.duration;
    }

    public void OnObjectReused() {
        Play();
        Invoke("Stop", _duration);
    }

    public void Play() {
        _vfx.Play();
    }

    public void Stop() {
        _vfx.Stop();
    }

}
