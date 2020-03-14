using UnityEngine;

public class GhostEffect : MonoBehaviour {

    public float ghostDelay;
    public GameObject ghost;
    public bool makeGhost = false;

    private float _ghostDelaySeconds;

    private void Start() {
        _ghostDelaySeconds = ghostDelay;
    }

    private void Update() {
        if (makeGhost) {
            if (_ghostDelaySeconds > 0) {
                _ghostDelaySeconds -= Time.deltaTime;

            } else {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                _ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
