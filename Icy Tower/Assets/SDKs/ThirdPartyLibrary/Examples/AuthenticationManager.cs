using UnityEngine;
using Library.Authentication.GooglePlay;
using Library.Authentication;
using System;

public class AuthenticationManager : MonoBehaviour {

    private PlayfabCustomAuth _customAuth;
    private GooglePlayGameService _gpgsAuth;

    public static AuthenticationManager instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _customAuth = new PlayfabCustomAuth();
        _gpgsAuth = new GooglePlayGameService();
    }

    /**************************************************************************************************/
    // AUTHENTICATION

    public void AuthenticateToPlayFab(Action<bool, string> actionResultCallback = null) {
        Debug.Log("Mobile Device login in progress...");

        /// Login as Guest with Unique DeviceID
        _customAuth.AnonymousLogin(
            linkAction: false,

            (actionResult, actionMessage) => {
                actionResultCallback?.Invoke(actionResult, actionMessage);
            });
    }

    public void AuthenticateToGPGS(Action<bool, string> actionResultCallback = null) {
        Debug.Log("GPGS login in progress...");

        _gpgsAuth.LoginPlayGameService(
            linkAction: false,
            (actionResult, actionMessage, actionRecover) => {
                actionResultCallback?.Invoke(actionResult, actionMessage);
            });
    }

}
