using UnityEngine;

public class DailyRewardsButtonTween : MonoBehaviour {

    [SerializeField]
    private LeanTweenType _type;

    [SerializeField]
    private float _time = 1f;

    [SerializeField]
    private float _scale = 1.2f;

    public void StartScaleAnim() {
        LeanTween.scale(gameObject, Vector3.one * _scale, _time).setEase(_type).setLoopPingPong();
    }

    public void StopScaleAnim() {
        LeanTween.cancel(gameObject);
    }

}
