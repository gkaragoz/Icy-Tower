using UnityEngine;

public class SuperCoin : MonoBehaviour, IPooledObject {

    [Utils.ReadOnly]
    [SerializeField]
    private SuperCoinStats _superCoinStats = null;

    private void Start() {
        _superCoinStats = GetComponent<SuperCoinStats>();
    }

    private void PlayVFX() {
        ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXCollectGold.ToString(), transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            PlayVFX();
            GameManager.instance.AddGoldToPlayer(_superCoinStats.GetAmount());
            gameObject.SetActive(false);
        }
    }

    public void OnObjectReused() {
        gameObject.SetActive(true);
    }
}
