using UnityEngine;
using TMPro;

public class TextLocaliserUI : MonoBehaviour {

    public LocalisedString localisedString;

    private TextMeshProUGUI _textField;

    private void Start() {
        _textField = GetComponent<TextMeshProUGUI>();
        
        ChangeText();

        LocalizationSystem.OnLanguageChanged += ChangeText;
    }

    public void ChangeText() {
        _textField.text = localisedString.value;
    }


}
