using TMPro;
using UnityEngine;

public class PnlPopupOpenClose : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _txtHeader = null;
    [SerializeField]
    private TextMeshProUGUI _txtMessage = null;

    public void SetText(string header, string messageOrKey) {
        _txtHeader.text = header;
        _txtMessage.text = messageOrKey;
    }

}
