using UnityEngine;

public class Key : MonoBehaviour{
    
    [Utils.ReadOnly]
    [SerializeField]
    private KeyStats _keyStats= null;

    private void Start() {
        _keyStats = GetComponent<KeyStats>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "FullCollider") {
            gameObject.SetActive(false);
            GameManager.instance.AddKeyToPlayer(_keyStats.GetAmount());
        }
    }

}
