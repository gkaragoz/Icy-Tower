using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionServicesUI : MonoBehaviour {

    private void Awake() {
        _btnConnectGooglePlay.onClick.AddListener(() => { ConnectToGoogleplay(); });

        _btnConnectFacebook.onClick.AddListener(() => { ConnectToFacebook(); });
    }

    [Header("Connect Service Configuration")]
    [SerializeField]
    private Button _btnConnectGooglePlay = null;
    [SerializeField]
    private Button _btnConnectFacebook = null;
    [SerializeField]
    private TextMeshProUGUI _txtGooglePlay = null;
    [SerializeField]
    private TextMeshProUGUI _txtFacebook = null;
    private void Start() {
        if (Library.GooglePlay.GooglePlayGameService.LoggedIn) {
            // Set button text.
            _txtGooglePlay.text = "Bağlı";
        } else {
            // Set button text.
            _txtGooglePlay.text = "Bağlı Değil";
        }
        if (Facebook.Unity.FB.IsLoggedIn) {
            // Set button text.
            _txtFacebook.text = "Bağlı";
        } else {
            // Set button text.
            _txtFacebook.text = "Bağlı Değil";
        }
    }

    public void ConnectToGoogleplay() {
        _btnConnectGooglePlay.interactable = false;

        ConnectionServices.instance.ConnectGooglePlay(() => {
            if (Library.GooglePlay.GooglePlayGameService.LoggedIn) {
                // Set button text.
                _txtGooglePlay.text = "Bağlı";
                _btnConnectGooglePlay.interactable = true;
            } else {
                // Set button text.
                _txtGooglePlay.text = "Bağlı Değil";
                _btnConnectGooglePlay.interactable = true;
            }
        });
    }

    public void ConnectToFacebook() {
        ConnectionServices.instance.ConnectFacebook(() => {
            if (Facebook.Unity.FB.IsLoggedIn) {
                // Set button text.
                _txtFacebook.text = "Bağlı";
                _btnConnectFacebook.interactable = true;
            } else {
                // Set button text.
                _txtFacebook.text = "Bağlı Değil";
                _btnConnectFacebook.interactable = true;
            }
        });
    }


}
