using UnityEngine;

public class SuperCoin : MonoBehaviour {

    [Utils.ReadOnly]
    [SerializeField]
    private SuperCoinStats _superCoinStats = null;

    private void Start() {
        _superCoinStats = GetComponent<SuperCoinStats>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            gameObject.SetActive(false);
            GameManager.instance.AddGoldToPlayer(_superCoinStats.GetAmount());
        }
    }
}
