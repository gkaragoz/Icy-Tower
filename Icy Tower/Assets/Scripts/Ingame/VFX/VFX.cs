using UnityEngine;

public class VFX : MonoBehaviour {

    [SerializeField]
    private ParticleSystem[] _vfxs = null;
    [SerializeField]
    private VFXTypes _vfxType = VFXTypes.CollectGold;

    [SerializeField]
    [Utils.ReadOnly]
    private float _duration = 0f;

    public VFXTypes VFXType { 
        get {
            return _vfxType;
        }
    }

    private void Awake() {
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
        for (int ii = 0; ii < _vfxs.Length; ii++) {
            Destroy(this.gameObject);
        }
    }

}
