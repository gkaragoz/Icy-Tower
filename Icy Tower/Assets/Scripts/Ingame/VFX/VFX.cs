using UnityEngine;

public class VFX : MonoBehaviour {

    [SerializeField]
    private VFXTypes _vfxType = VFXTypes.CollectGold;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private ParticleSystem[] _vfxs = null;
    [SerializeField]
    [Utils.ReadOnly]
    private float _duration = 0f;

    public VFXTypes VFXType { 
        get {
            return _vfxType;
        }
    }

    private void Awake() {
        _vfxs = GetComponentsInChildren<ParticleSystem>();

        _duration = _vfxs[0].main.duration;
    }

    public void Play(bool manualStop = false) {
        for (int ii = 0; ii < _vfxs.Length; ii++) {
            _vfxs[ii].Play();

            if (manualStop == false) {
                Invoke("Stop", _duration);
            }
        }
    }

    public void Stop() {
        Destroy(this.gameObject);
    }

}
