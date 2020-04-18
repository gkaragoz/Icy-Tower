using TMPro;
using UnityEngine;

public class SettingsPanel : MonoBehaviour{

    [SerializeField]
    private GameObject _generalSettings = null;
    [SerializeField]
    private GameObject _languageSettings = null;
    [SerializeField]
    private GameObject _pnlClose = null;

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

}
