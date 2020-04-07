using UnityEngine;

public class SettingsPanel : MonoBehaviour{

    [SerializeField]
    private GameObject _joystickPanel = null;
    [SerializeField]
    private GameObject _buttonsPanel = null;
    [SerializeField]
    private GameObject _mainMenuPanel = null;

    public void UseButtons() {
        _joystickPanel.SetActive(false);
        _buttonsPanel.SetActive(true);
    }

    public void UseJoystick() {
        _joystickPanel.SetActive(true);
        _buttonsPanel.SetActive(false);
    }

}
