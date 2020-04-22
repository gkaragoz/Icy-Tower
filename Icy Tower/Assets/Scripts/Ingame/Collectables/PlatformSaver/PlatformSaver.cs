using UnityEngine;

public class PlatformSaver : MonoBehaviour, IHaveSingleSound {

    [Utils.ReadOnly]
    [SerializeField]
    private PlatformSaverStats _platformSaverStats = null;
    [Utils.ReadOnly]
    [SerializeField]
    private int _platformCountToMaximize;


    private void Start() {
        _platformSaverStats = GetComponent<PlatformSaverStats>();
        _platformCountToMaximize = _platformSaverStats.GetPlatformCount();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlatformSaver") {
            _platformCountToMaximize = _platformSaverStats.GetPlatformCount();
            MaximizePlatformScale();
            PlaySFX(SoundFXTypes.InGame_PowerUp_BlockSaver);
            other.gameObject.SetActive(false);
        }
    }

    private void MaximizePlatformScale() {
        for (int i = 0; i < _platformCountToMaximize; i++) {
            Platform platformToMaximize = PlatformManager.instance.GetSpawnedPlatformAtFloor(Account.instance.GetCurrentScore() + i);
            GameObject childPlatform = platformToMaximize.Types[PlatformManager.instance.PlatformTypeIndex];
            childPlatform.transform.localPosition = Vector3.zero;
            childPlatform.transform.localScale = new Vector3(childPlatform.transform.localScale.x, childPlatform.transform.localScale.y, 10f);
        }
    }


    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
}
