using TMPro;
using UnityEngine;

public class SettingsPanel : MonoBehaviour{

    [SerializeField]
    private GameObject _generalSettings = null;
    [SerializeField]
    private GameObject _languageSettings = null;
    [SerializeField]
    private GameObject _pnlClose = null;
    [SerializeField]
    private ControllerButton _controllerButtonScript = null;

    [SerializeField]
    private GameObject _joystickController = null;
    [SerializeField]
    private GameObject _buttonController = null;

    [SerializeField]
    private ControllerType _selectedControllerType = ControllerType.Joystick;

    private bool _isJoystickActive = true;

    public void OpenLanguageSettings() {
        _generalSettings.SetActive(false);
        _languageSettings.SetActive(true);
        _pnlClose.SetActive(false);
    }

    public void OpenGeneralSettings() {
        _generalSettings.SetActive(true);
        _languageSettings.SetActive(false);
        _pnlClose.SetActive(true);
    }

    public void ChangeController() {
        _isJoystickActive = !_isJoystickActive;

        if (_isJoystickActive) {
            SetController(ControllerType.Joystick);
        } else {
            SetController(ControllerType.Button);
        }
    }

    public void SetController(ControllerType controllerType) {
        this._selectedControllerType = controllerType;

        switch (controllerType) {
            case ControllerType.Joystick:
                _isJoystickActive = true;
                break;
            case ControllerType.Button:
                _isJoystickActive = false;
                break;
            default:
                _isJoystickActive = true;
                break;
        }

        if (_isJoystickActive) {
            this._selectedControllerType = ControllerType.Joystick;

            _controllerButtonScript.SwitchToJoystick();
            ActivateJoystickControllers();
        } else {
            this._selectedControllerType = ControllerType.Button;

            _controllerButtonScript.SwitchToButton();
            ActivateButtonControllers();
        }
    }

    private void ActivateJoystickControllers() {
        _buttonController.SetActive(false);
        _joystickController.SetActive(true);
    }

    private void ActivateButtonControllers() {
        _buttonController.SetActive(true);
        _joystickController.SetActive(false);
    }

}
