using UnityEngine;

public class ControllerButton : MonoBehaviour {

    [SerializeField]
    private GameObject _joystickText;
    [SerializeField]
    private GameObject _buttonText;

    public void SwitchToJoystick() {
        _joystickText.SetActive(true);
        _buttonText.SetActive(false);
    }

    public void SwitchToButton() {
        _joystickText.SetActive(false);
        _buttonText.SetActive(true);
    }

}
