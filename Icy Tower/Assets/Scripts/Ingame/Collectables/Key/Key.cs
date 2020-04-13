using UnityEngine;

public class Key : MonoBehaviour, IHaveSingleSound{
    
    [Utils.ReadOnly]
    [SerializeField]
    private KeyStats _keyStats= null;

    private void Start() {
        _keyStats = GetComponent<KeyStats>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_Key);
            GameManager.instance.AddKeyToPlayer(_keyStats.GetAmount());
            gameObject.SetActive(false);
        }
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
    private void PlayVFX() {
        ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXCollectGold.ToString(), transform.position);
    }
}
