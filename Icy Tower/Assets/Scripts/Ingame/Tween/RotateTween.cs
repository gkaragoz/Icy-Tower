using UnityEngine;

public class RotateTween : MonoBehaviour {

    [SerializeField]
    private float _time = 1f;

    private void Start() {
        _time = Random.Range(0.8f, 1f);

        LeanTween.rotateAround(gameObject, Vector3.up, 360, _time).setLoopClamp();
    }

}
