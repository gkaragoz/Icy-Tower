using UnityEngine;

public class IngameUITween : MonoBehaviour {

    public float idleSpeed = 0.5f;
    public float idleRotationValue = 10f;
    public float idleScale = 0.2f;

    private Vector3 _initialScale = Vector3.zero;

    private void Start() {
        _initialScale = transform.localScale;
        transform.localScale = Vector3.zero;

        LeanTween.scale(gameObject, _initialScale, 0.2f).setDelay(Random.Range(1f, 1.45f)).setEaseInBounce().setIgnoreTimeScale(true).setOnComplete(PlayHello);
    }

    private void PlayHello() {
        LeanTween.rotateZ(gameObject, idleRotationValue, idleSpeed).setLoopPingPong().setIgnoreTimeScale(true).setEaseLinear();
    }

}
