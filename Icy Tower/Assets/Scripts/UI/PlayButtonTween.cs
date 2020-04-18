using UnityEngine;

public class PlayButtonTween : MonoBehaviour {

    [SerializeField]
    private LeanTweenType _type;

    [SerializeField]
    private float _time = 1f;

    [SerializeField]
    private float _scale = 1.2f;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            StartScaleAnim();
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            StopScaleAnim();
        }
    }

    public void StartScaleAnim() {
        LeanTween.scale(gameObject, Vector3.one * _scale, _time).setEase(_type).setLoopPingPong().setIgnoreTimeScale(true);
    }

    public void StopScaleAnim() {
        LeanTween.cancel(gameObject);

        transform.localScale = Vector3.one;
    }

}
