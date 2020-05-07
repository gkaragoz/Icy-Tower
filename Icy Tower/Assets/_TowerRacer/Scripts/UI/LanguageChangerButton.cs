using UnityEngine;

public class LanguageChangerButton : MonoBehaviour {

    [SerializeField]
    private LocalizationSystem.Language _language = LocalizationSystem.Language.English;

    public void ChangeLanguage() {
        LocalizationSystem.ChangeLanguage(_language);

        PlayerPrefs.SetString(Strings.PP_LANGUAGE, _language.ToString());
    }

}
