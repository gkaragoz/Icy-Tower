using UnityEngine;
using Library.Authentication.GooglePlay;
using Library.Authentication;
using System;

public class AuthenticationManager : MonoBehaviour {

    private PlayfabCustomAuth _customAuth;
    private GooglePlayGameService _gpgsAuth;

    [SerializeField]
    private LoadManager _loadManager = null;

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

    public void InitAuth(Action<AuthenticationEventType, AuthenticationType> callback) {
        Debug.Log("Accounts are checking...");

        // LoggedIn before with GPGS
        if (_gpgsAuth.LoggedInBefore()) {
            Debug.Log("An account found, trying to login...");
            callback(AuthenticationEventType.Begin, AuthenticationType.GooglePlayGameServices);

            /// GooglePlayGameService handles automatically Login with GPGS
            Debug.Log("GPGS login in...");
            // Link GPGS Acc.
            _gpgsAuth.LoginPlayGameService(
                linkAction: false,
                (actionResult, actionMessage, actionRecover) => {
                    if (actionResult) // Request completed with no error ( Succeed )
                    {
                        callback(AuthenticationEventType.Success, AuthenticationType.GooglePlayGameServices);

                        Debug.Log("GPGS login succeed: " + actionMessage);
                    } else // Request completed failure
                    {
                        callback(AuthenticationEventType.Failed, AuthenticationType.GooglePlayGameServices);

                        Debug.Log("GPGS login failed: " + actionMessage);
                    }
                });
        }

        // Not LoggedIn before with GPGS
        else {
            Debug.Log("Trying to preparing credentials for anonymous account and login in...");
            callback(AuthenticationEventType.Begin, AuthenticationType.PlayFab);

            Debug.Log("Anonymous login in...");
            /// Login as Guest with Unique DeviceID
            _customAuth.AnonymousLogin(
                linkAction: false,
                (actionResult, actionMessage) => {
                    if (actionResult) // Request completed with no error ( Succeed )
                    {
                        callback(AuthenticationEventType.Success, AuthenticationType.PlayFab);

                        Debug.Log("Anonymous succeed: " + actionMessage);
                    } else // Request completed failure
                    {
                        callback(AuthenticationEventType.Failed, AuthenticationType.PlayFab);

                        Debug.Log("Anonymous login failed: " + actionMessage);
                    }
                });
        }
    }

}
