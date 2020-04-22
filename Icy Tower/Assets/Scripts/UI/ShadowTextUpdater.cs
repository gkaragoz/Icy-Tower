using TMPro;
using UnityEngine;

public class ShadowTextUpdater : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txt = null;
    [SerializeField]
    private TextMeshProUGUI _txtShadow = null;

    public void SetText(string str) {
        _txt.text = str;
        _txtShadow.text = str;
    }
    
}
