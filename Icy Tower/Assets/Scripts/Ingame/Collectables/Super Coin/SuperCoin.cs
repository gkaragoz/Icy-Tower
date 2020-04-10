using UnityEngine;

public class SuperCoin : MonoBehaviour {

    [Utils.ReadOnly]
    [SerializeField]
    private SuperCoinStats _superCoinStats = null;

    private void Start() {
        _superCoinStats = GetComponent<SuperCoinStats>();
    }

    private void PlayVFX() {
        VFX activeVFX = Instantiate(VFXDatabase.instance.GetVFX(VFXTypes.CollectGold));
        activeVFX.transform.position = transform.position;
        activeVFX.Play();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            PlayVFX();
            gameObject.SetActive(false);
            GameManager.instance.AddGoldToPlayer(_superCoinStats.GetAmount());
        }
    }
}
