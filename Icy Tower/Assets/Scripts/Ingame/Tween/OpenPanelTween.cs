using UnityEngine;

public class OpenPanelTween : MonoBehaviour {

    [SerializeField]
    private bool _noTween = false;

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

    public void OpenPanel() {
        if (_noTween) {
            transform.localScale = Vector3.one * _openingScale;
            SetActiveTrue();
            return;
        }

        transform.localScale = Vector3.one * _closingScale;
        SetActiveTrue();

        LeanTween.scale(gameObject, Vector3.one * _openingScale, _openingTime).setEase(_openingType).setIgnoreTimeScale(true);
    }

    public void ClosePanel() {
        if (_noTween) {
            SetActiveFalse();
            return;
        }

        LeanTween.scale(gameObject, Vector3.one * _closingScale, _closingTime).setEase(_closingType).setIgnoreTimeScale(true).setOnComplete(SetActiveFalse);
    }

    private void SetActiveTrue() {
        this.gameObject.SetActive(true);
    }

    private void SetActiveFalse() {
        this.gameObject.SetActive(false);
    }

}
