using UnityEngine;
using Library.GooglePlay;
using Library.FaceBook;
using System;

public class ConnectionServices : MonoBehaviour {

    #region Singleton

    public static ConnectionServices instance;
    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _facebookAuth = new FacebookService();
        _gpgsAuth = new GooglePlayGameService();
    }

    #endregion

    private FacebookService _facebookAuth;
    private GooglePlayGameService _gpgsAuth;

    // Connect Google Play
    public void ConnectGooglePlay(Action onFinished) {
        if (GooglePlayGameService.LoggedIn) // Loggedin with GPGS
        {
            // UnLink GPGS Request
            _gpgsAuth.UnLinkWithGooglePlayAccount(
                (actionResult, actionMessage) => {
                    if (actionResult) // Request completed success
                    {
                        Debug.Log(actionMessage);

                        onFinished();
                    } else // Request completed failure
                    {
                        Debug.LogError(actionMessage);

                        onFinished();
                    }
                });
        } else // Not Loggedin with GPGS, Connect with it.
        {
            // Link GPGS Acc.
            _gpgsAuth.LoginPlayGameService(
                linkAction: true,

                (actionResult, actionMessage, actionRecover) => {
                    if (actionResult) // Request completed with no error ( Succeed )
                    {
                        Debug.Log(actionMessage);
                        onFinished();
                    } else // Request completed failure
                    {
                        Debug.Log(actionMessage);
                        onFinished();
                    }
                });
        }
    }

    // Connect Facebook
    public void ConnectFacebook(Action<bool> isLinkedCallback) {
        if (FacebookService.GetLoggedIn()) // Loggedin with Facebook
        {
            // UnLink Facebook Acc.
            _facebookAuth.UnLinkWithFacebook(

                (actionResult, actionMessage) => {
                    if (actionResult) // Request completed success
                    {
                        Debug.Log(actionMessage);
                        isLinkedCallback(false);
                    } else // Request completed failure
                    {
                        Debug.LogError(actionMessage);
                        isLinkedCallback(true);
                    }
                });
        } else {
            // Link Facebook Acc.
            _facebookAuth.AuthLogin(

                linkAction: true,

                (actionResult, actionMessage, actionRecover) => {
                    if (actionResult) // Request completed with no error ( Succeed )
                    {
                        Debug.Log(actionMessage);
                        isLinkedCallback(true);
                    } else // Request completed failure
                    {
                        Debug.Log(actionMessage);
                        isLinkedCallback(false);
                    }
                });
        }
    }

}
