using UnityEngine;

public class OpenPanelTween : MonoBehaviour {

    [SerializeField]
    private LeanTweenType _openingType;

    [SerializeField]
    private float _openingTime = 1f;

    [SerializeField]
    private float _openingScale = 1.2f;

    [SerializeField]
    private LeanTweenType _closingType;

    [SerializeField]
    private float _closingTime = 1f;

    [SerializeField]
    private float _closingScale = 1.2f;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            OpenPanel();
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            ClosePanel();
        }
    }

    public void OpenPanel() {
        LeanTween.scale(gameObject, Vector3.one * _openingScale, _openingTime).setEase(_openingType).setIgnoreTimeScale(true);
    }

    public void ClosePanel() {
        LeanTween.scale(gameObject, Vector3.one * _closingScale, _closingTime).setEase(_closingType).setIgnoreTimeScale(true);
    }

}
