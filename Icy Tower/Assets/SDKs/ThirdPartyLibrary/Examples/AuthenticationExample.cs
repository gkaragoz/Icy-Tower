using UnityEngine;
using Library.Authentication.GooglePlay;
using Library.Authentication;

public class AuthenticationExample : MonoBehaviour
{
    private PlayfabCustomAuth customAuth;

    private GooglePlayGameService gpgsAuth;

    private void Awake()
    {
        customAuth = new PlayfabCustomAuth();

        gpgsAuth = new GooglePlayGameService();
    }

    /**************************************************************************************************/
    // AUTHENTICATION

    private void Start()
    {
        // LoggedIn before with GPGS
        if (gpgsAuth.LoggedInBefore())
        {
            /// GooglePlayGameService handles automatically Login with GPGS
            Debug.Log("GPGS login in automatically...");

            // Link GPGS Acc.
            gpgsAuth.LoginPlayGameService(
                linkAction: false,
                (actionResult, actionMessage, actionRecover) =>
                {
                    if (actionResult) // Request completed with no error ( Succeed )
                    {
                        
                    }
                    else // Request completed failure
                    {
                       
                    }
                });
        }
        // Not LoggedIn before with GPGS
        else
        {
            Debug.Log("Mobile Device login automatically...");

            /// Login as Guest with Unique DeviceID
            customAuth.AnonymousLogin(
                linkAction: false,

                (actionResult, actionMessage) =>
                {
                    if (actionResult) // Request completed with no error ( Succeed )
                    {

                    }

                    else // Request completed failure
                    {

                    }
                });
        }
    }

}
