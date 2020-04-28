using UnityEngine;

public class Key : MonoBehaviour, IHaveSingleSound,IPooledObject{
  

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            PlayVFX();
            PlaySFX(SoundFXTypes.InGame_Collect_Key);
            
            Account.instance.AddVirtualCurrency(1, VirtualCurrency.Key);
            gameObject.SetActive(false);
        }
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
    private void PlayVFX() {
        ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXCollectGold.ToString(), transform.position);
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
    }
}
