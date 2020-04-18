using UnityEngine;

public class ControllerButton : MonoBehaviour {

    [SerializeField]
    private GameObject _joystickText;
    [SerializeField]
    private GameObject _buttonText;

    [SerializeField]
    private ControllerType _selectedControllerType = ControllerType.Joystick;

    private bool _isJoystickActive = true;

    private void SwitchToJoystick() {
        _joystickText.SetActive(true);
        _buttonText.SetActive(false);
    }

    private void SwitchToButton() {
        _joystickText.SetActive(false);
        _buttonText.SetActive(true);
    }

    public void ChangeController() {
        _isJoystickActive = !_isJoystickActive;

        if (_isJoystickActive) {
            this._selectedControllerType = ControllerType.Joystick;

            SwitchToJoystick();
        } else {
            this._selectedControllerType = ControllerType.Button;

            SwitchToButton();
        }
    }

}
