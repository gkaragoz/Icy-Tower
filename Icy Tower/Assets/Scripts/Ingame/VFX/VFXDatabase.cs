using UnityEngine;

public class VFXDatabase : MonoBehaviour {

    #region Singleton

    public static VFXDatabase instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    private VFX[] _vfxList = null;
    
    public VFX GetVFX(VFXTypes vfxType) {
        for (int ii = 0; ii < _vfxList.Length; ii++) {
            if (_vfxList[ii].VFXType == vfxType) {
                return _vfxList[ii];
            }
        }
        return null;
    }

}
