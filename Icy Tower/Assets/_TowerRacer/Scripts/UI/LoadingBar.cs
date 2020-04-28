using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {

    [SerializeField]
    private LoadManager _loadManager = null;

    private Image _image;
    private int _totalProgress = 0;
    private int _incrementValue = 0;

    private List<Action> _progressActions = new List<Action>();

    private void Awake() {
        _image = GetComponent<Image>();

        _loadManager.OnAccountLoaded += OnTotalProgressChanged;
        _loadManager.OnPoolLoaded += OnTotalProgressChanged;
        _loadManager.OnSceneReady += OnTotalProgressChanged;

        _progressActions.Add(_loadManager.OnAccountLoaded);
        _progressActions.Add(_loadManager.OnPoolLoaded);
        _progressActions.Add(_loadManager.OnSceneReady);

        _incrementValue = 100 / _progressActions.Count;
    }

    private void OnTotalProgressChanged() {
        _totalProgress += _incrementValue;

        if (_totalProgress >= 99) {
            _totalProgress = 100;

            _loadManager.OnAccountLoaded -= OnTotalProgressChanged;
            _loadManager.OnPoolLoaded -= OnTotalProgressChanged;
            _loadManager.OnSceneReady -= OnTotalProgressChanged;
        }

        UpdateFillBar();
    }

    private void UpdateFillBar() {
        _image.fillAmount = _totalProgress / 100;
    }

}
