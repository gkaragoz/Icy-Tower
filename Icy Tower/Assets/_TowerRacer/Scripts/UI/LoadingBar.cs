using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {

    [SerializeField]
    private LoadManager _loadManager = null;
    [SerializeField]
    private TextMeshProUGUI _txtMessage = null;

    private Image _image;
    private float _totalProgress = 0;
    private int _completedActionsCount = 0;

    private List<Action> _progressActions = new List<Action>();

    private void Awake() {
        _image = GetComponent<Image>();

        _loadManager.OnGPGSAccountInitializationBegin += OnGPGSAccountInitializationBegin;
        _loadManager.OnGPGSAccountInitializationSuccess += OnGPGSAccountInitializationSuccess;
        _loadManager.OnGPGSAccountInitiailzationFailed += OnGPGSAccountInitiailzationFailed;

        _loadManager.OnPlayFabAccountInitializationBegin += OnPlayFabAccountInitializationBegin;
        _loadManager.OnPlayFabAccountInitializationSuccess += OnPlayFabAccountInitializationSuccess;
        _loadManager.OnPlayFabAccountInitiailzationFailed += OnPlayFabAccountInitiailzationFailed;

        _loadManager.OnAccountLoaded += OnAccountLoaded;
        _loadManager.OnPoolLoaded += OnPoolLoaded;
        _loadManager.OnSceneReady += OnSceneReady;

        _progressActions.Add(_loadManager.OnGPGSAccountInitializationBegin);
        _progressActions.Add(_loadManager.OnGPGSAccountInitializationSuccess);
        _progressActions.Add(_loadManager.OnGPGSAccountInitiailzationFailed);

        _progressActions.Add(_loadManager.OnPlayFabAccountInitializationBegin);
        _progressActions.Add(_loadManager.OnPlayFabAccountInitializationSuccess);
        _progressActions.Add(_loadManager.OnPlayFabAccountInitiailzationFailed);

        _progressActions.Add(_loadManager.OnAccountLoaded);
        _progressActions.Add(_loadManager.OnPoolLoaded);
        _progressActions.Add(_loadManager.OnSceneReady);

        _totalProgress = 0;
        _completedActionsCount = 0;

        SetMessage(Strings.ON_GAME_STARTING);

        UpdateFillBar();
    }

    private void OnAccountLoaded() {
        SetMessage(Strings.ON_ACCOUNT_LOADED_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnPoolLoaded() {
        SetMessage(Strings.ON_POOL_LOADED_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnSceneReady() {
        SetMessage(Strings.ON_SCENE_READY_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnGPGSAccountInitializationBegin() {
        _progressActions.Remove(_loadManager.OnPlayFabAccountInitializationBegin);
        _progressActions.Remove(_loadManager.OnPlayFabAccountInitializationSuccess);
        _progressActions.Remove(_loadManager.OnPlayFabAccountInitiailzationFailed);

        SetMessage(Strings.ON_GPGS_ACCOUNT_INITIALIZATION_BEGIN_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnGPGSAccountInitializationSuccess() {
        SetMessage(Strings.ON_GPGS_ACCOUNT_INITIALIZATION_SUCCESS_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnGPGSAccountInitiailzationFailed() {
        _progressActions.Remove(_loadManager.OnGPGSAccountInitializationBegin);
        _progressActions.Remove(_loadManager.OnGPGSAccountInitializationSuccess);
        _progressActions.Remove(_loadManager.OnGPGSAccountInitiailzationFailed);

        SetMessage(Strings.ON_GPGS_ACCOUNT_INITIALIZATION_FAILED_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnPlayFabAccountInitializationBegin() {
        _progressActions.Remove(_loadManager.OnGPGSAccountInitializationBegin);
        _progressActions.Remove(_loadManager.OnGPGSAccountInitializationSuccess);
        _progressActions.Remove(_loadManager.OnGPGSAccountInitiailzationFailed);

        SetMessage(Strings.ON_PLAYFAB_ACCOUNT_INITIALIZATION_BEGIN_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnPlayFabAccountInitializationSuccess() {
        SetMessage(Strings.ON_PLAYFAB_ACCOUNT_INITIALIZATION_SUCCESS_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnPlayFabAccountInitiailzationFailed() {
        _progressActions.Remove(_loadManager.OnPlayFabAccountInitializationBegin);
        _progressActions.Remove(_loadManager.OnPlayFabAccountInitializationSuccess);
        _progressActions.Remove(_loadManager.OnPlayFabAccountInitiailzationFailed);

        SetMessage(Strings.ON_PLAYFAB_ACCOUNT_INITIALIZATION_FAILED_MESSAGE);

        OnTotalProgressChanged();
    }

    private void OnTotalProgressChanged() {
        _completedActionsCount++;
        _totalProgress = 20 * _completedActionsCount;

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

    private void SetMessage(string message) {
        _txtMessage.text = message;
    }

}
